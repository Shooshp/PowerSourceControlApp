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
        public TcpClient tcp;

        public PowerSource(string server)
        {
            Server = server;
            ChanelList = new List<Chanel>();
            tcp = new TcpClient();

            var connectionString = new MySqlConnectionStringBuilder
            {
                Server = Server,
                UserID = "root",
                Password = "123",
                Database = "local_data_storage"
            };

            using (var connection = GetConnection(connectionstring: connectionString))
            {
                var chanels = connection.GetList<PowerSourceSettings>();

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, Server));
                }
            }

            foreach (var chanel in ChanelList)
            {
                var initThread = new Thread(() => chanel.Init());
                initThread.Start();
            }

            IsOnline = true;
        }

        private static MySqlConnection GetConnection(MySqlConnectionStringBuilder connectionstring)
        {
            var connection = new MySqlConnection(connectionstring.ToString());
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
                        Console.WriteLine("Ping: " + Server);
                    }
                    catch (Exception e)
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