using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DevExpress.XtraPrinting.Native;

namespace PowerSourceControlApp
{
    public class NetworkDeviceDetector
    {
        private Thread _udpReadThread;
        private volatile bool _terminateThread;

        public event DataEventHandler OnDataReceived;

        public delegate void DataEventHandler(object sender, DataEventArgs e);

        public void CreateUdpReadThread()
        {
            _udpReadThread = new Thread(UdpReadThread) {Name = "UDP Listener"};
            _udpReadThread.Start(new IPEndPoint(IPAddress.Any, 10237));
        }

        private void UdpReadThread(object endPoint)
        {
            var myEndPoint = (EndPoint) endPoint;
            var udpListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
            udpListener.Bind(myEndPoint);

            try
            {
                while (!_terminateThread)
                {
                    try
                    {
                        var buffer = new byte[1024];
                        var size = udpListener.ReceiveFrom(buffer, ref myEndPoint);
                        Array.Resize(ref buffer, size);
                        OnDataReceived?.Invoke(((IPEndPoint)(myEndPoint)).Address,new DataEventArgs(((IPEndPoint)(myEndPoint)).Address, buffer));
                    }
                    catch (SocketException socketException)
                    {
                    }
                }
            }
            finally
            {
                udpListener.Shutdown(SocketShutdown.Both);
                udpListener.Close();
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
