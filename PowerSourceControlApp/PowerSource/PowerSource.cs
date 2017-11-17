using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DeviceManagment;

namespace PowerSourceControlApp
{
    public class PowerSource
    {
        public string IpAddress { get; }
        public List<Chanel> ChanelList;
        public bool IsOnline { get; set; }
        private MySqlConnectionStringBuilder connectionString;
        public string Status;
        public readonly int StatusPort;
        public DeviceManager Collection;
        public StatusChecker Pinger;


        public PowerSource(string ipAddress, DeviceManager collection)
        {
            Pinger = new StatusChecker(this);
            IpAddress = ipAddress;
            Collection = collection;
            StatusPort = 10236;
            ChanelList = new List<Chanel>();
            Status = "Inited";
 

            connectionString = new MySqlConnectionStringBuilder
            {
                Server = IpAddress,
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
            Pinger.Start();
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(connectionString.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }
    }
}