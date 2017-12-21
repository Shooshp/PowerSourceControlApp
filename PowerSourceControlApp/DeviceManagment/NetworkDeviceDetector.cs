using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PowerSourceControlApp.DeviceManagment
{
    public class NetworkDeviceDetector : IDisposable
    {
        private Thread _udpReadThread;
        private Socket _udpListener;
        private EndPoint _myEndPoint;
        public volatile bool SuspendThread;

        public event DataEventHandler OnDataReceived;

        public delegate void DataEventHandler(object sender, DataEventArgs e);

        public void CreateUdpReadThread()
        {
            SuspendThread = false;
            _udpReadThread = new Thread(UdpReadThread)
            {
                Name = "UDP Listener",
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };
            _udpReadThread.Start(new IPEndPoint(IPAddress.Any, 10237));
        }

        private void UdpReadThread(object endPoint)
        {
            if (_myEndPoint == null)
            {
                _myEndPoint = (EndPoint)endPoint;
            }
            
            _udpListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _udpListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
            _udpListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _udpListener.Bind(_myEndPoint);

            while (!SuspendThread)
            {
                Thread.Sleep(100);
                if (_udpListener.Available != 0)
                {
                    try
                    {
                        var buffer = new byte[1024];
                        var size = _udpListener.ReceiveFrom(buffer, ref _myEndPoint);
                        Array.Resize(ref buffer, size);
                        OnDataReceived?.Invoke(((IPEndPoint) (_myEndPoint)).Address,
                            new DataEventArgs(((IPEndPoint) (_myEndPoint)).Address, buffer));
                    }
                    catch (SocketException)
                    {
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    SuspendThread = true;
                    Thread.Sleep(110);
                    _udpListener.Close();
                    _udpReadThread.Abort();
                }
            }
            finally
            {
                _udpListener.Dispose();
            }
        }
    }

    public class DataEventArgs : EventArgs
    {
        public byte[] Data { get; private set; }
        public IPAddress IpAddress { get; private set; }

        public DataEventArgs(IPAddress ipaddress, byte[] data)
        {
            IpAddress = ipaddress;
            Data = data;
        }
    }
}
