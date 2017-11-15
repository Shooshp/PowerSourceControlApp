using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Dapper;
using MySql.Data.MySqlClient;

namespace PowerSourceControlApp
{
    public class PowerSource
    {
        public string Server { get; }
        public List<Chanel> ChanelList;
        public bool isOnline;

        public PowerSource(string server)
        {
            Server = server;
            ChanelList = new List<Chanel>();

            var connectionString = new MySqlConnectionStringBuilder
            {
                Server = Server,
                UserID = "root",
                Password = "123",
                Database = "local_data_storage"
            };

            using (var connection = GetConnection(connectionstring: connectionString))
            {
                connection.Open();
                var chanels = connection.GetList<PowerSourceSettings>();
                connection.Close();

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, Server));
                }
            }

            foreach (var chanel in ChanelList)
            {
                chanel.Init();
            }

            isOnline = true;
        }

        private static MySqlConnection GetConnection(MySqlConnectionStringBuilder connectionstring)
        {
            var connection = new MySqlConnection(connectionstring.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }

        public void Ping()
        {
            try
            {
                using (var client = new TcpClient(Server, 10236))
                {
                    isOnline = true;
                }
            }
            catch (Exception e)
            {
                isOnline = false;
            }
        }
    }
}