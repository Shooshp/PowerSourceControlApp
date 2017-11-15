using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace PowerSourceControlApp
{
    class NetworkScaner
    {
        public string BaseIp;
        public int port;
        public List<Pinger> PingerList;

        public NetworkScaner(string baseIp, int port)
        {
            PingerList = new List<Pinger>();
            this.BaseIp = baseIp;
            this.port = port;

            for (uint addressSuffix = 1; addressSuffix < 256; addressSuffix++)
            {
                var address = string.Concat(this.BaseIp, addressSuffix.ToString());
                PingerList.Add(new Pinger(address, this.port));
            }

            Thread.Sleep(100);
            PingerList.RemoveAll(x => x.IsOnline == false);
        }

        internal class Pinger
        {
            public bool IsOnline = false;
            public string address;
            public int port;

            public Pinger(string address, int port)
            {
                this.port = port;
                this.address = address;

                var pingThread = new Thread(CheckAddressAndPort);
                pingThread.IsBackground = true;
                pingThread.Start();
            }

            private void CheckAddressAndPort()
            {
                try
                {
                    using (var client = new TcpClient(this.address, this.port)) { IsOnline = true; }
                }
                catch (Exception e)
                {
                    IsOnline = false;
                }
            }
        }
    }
}
