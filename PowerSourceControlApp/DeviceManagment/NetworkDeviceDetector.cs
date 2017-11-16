using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PowerSourceControlApp
{
    public class NetworkDeviceDetector
    {
        private Thread _udpReadThread;
        public volatile bool SuspendThread;


        public event DataEventHandler OnDataReceived;

        public delegate void DataEventHandler(object sender, DataEventArgs e);

        public void CreateUdpReadThread()
        {
            SuspendThread = false;
            _udpReadThread = new Thread(UdpReadThread) {Name = "UDP Listener"};
            _udpReadThread.Start(new IPEndPoint(IPAddress.Any, 10237));
        }

        private void UdpReadThread(object endPoint)
        {
            var myEndPoint = (EndPoint) endPoint;
            var udpListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
            udpListener.Bind(myEndPoint);

            while (!SuspendThread)
            {
                if (udpListener.Available != 0)
                {
                    try
                    {
                        var buffer = new byte[1024];
                        var size = udpListener.ReceiveFrom(buffer, ref myEndPoint);
                        Array.Resize(ref buffer, size);
                        OnDataReceived?.Invoke(((IPEndPoint) (myEndPoint)).Address,
                            new DataEventArgs(((IPEndPoint) (myEndPoint)).Address, buffer));
                    }
                    catch (SocketException)
                    {
                    }
                }
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
