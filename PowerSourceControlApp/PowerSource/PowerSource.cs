using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;

namespace PowerSourceControlApp
{
    public class PowerSource
    {
        public string Server { get; }
        public List<Chanel> ChanelList;
        public bool IsOnline { get; set; }
        public bool IsBusy;
        private MySqlConnectionStringBuilder connectionString;
        public TcpClient Tcp;

        public PowerSource(string server)
        {
            Server = server;
            ChanelList = new List<Chanel>();
            Tcp = new TcpClient();

            connectionString = new MySqlConnectionStringBuilder
            {
                Server = Server,
                UserID = "root",
                Password = "123",
                Database = "local_data_storage"
            };

            using (var connection = GetConnection())
            {
                var chanels = connection.GetList<PowerSourceSettings>();

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, this));
                }
            }

            foreach (var chanel in ChanelList)
            {
                var initThread = new Thread(() => chanel.Init());
                initThread.Start();
            }

            IsOnline = true;
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(connectionString.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }

        public void Ping()
        {
            IsOnline = false;
            using (var tcpconn = new TcpClient())
            {
                if (!tcpconn.Connected)
                {
                    try
                    {
                        tcpconn.Connect(Server, 10236);
                        IsOnline = true;
                        Console.WriteLine(@"Ping: " + Server);
                    }
                    catch (Exception)
                    {
                        IsOnline = false;
                    }
                }
                IsOnline = true;
                tcpconn.Close();
                tcpconn.Dispose();
                GC.Collect();
            } 
        }
    }
}