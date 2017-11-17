using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DeviceManagment;
using Renci.SshNet;

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
        public SshClient SshConnector;
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);


        public PowerSource(string ipAddress, DeviceManager collection)
        {
            Pinger = new StatusChecker(this);
            ChanelList = new List<Chanel>();
            IpAddress = ipAddress;
            Collection = collection;
            StatusPort = 10236;
            Status = "Inited";


            SshConnector = new SshClient(IpAddress, "pi", "raspberry");

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
            GetRemoteSystemTime();
            Pinger.Start();
        }

        public void GetRemoteSystemTime()
        {
            var date = RunSSHCommand("date +%s");
            date = date.Remove(date.Length - 1);
            var time = epoch.AddSeconds(Convert.ToInt64(date)).ToLocalTime();
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(time);

            var diffinseconds = (DateTime.Now - time).TotalSeconds;
        }

        private string RunSSHCommand(string command)
        {
            SshConnector.Connect();
            var cmd = SshConnector.CreateCommand(command);
            var result = cmd.Execute();
            SshConnector.Disconnect();
            return result;
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(connectionString.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }
    }
}