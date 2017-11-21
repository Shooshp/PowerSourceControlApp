﻿using System;
using System.Collections.Generic;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DapperDTO;
using PowerSourceControlApp.DeviceManagment;
using Renci.SshNet;

namespace PowerSourceControlApp.PowerSource
{
    public class Device
    {
        public string IpAddress { get; }
        public string Status;
        public string Message;
        public string Reply;
        public bool IsOnline { get; set; }
        public bool SqlIsBusy;             
        public readonly int StatusPort;
        public List<Chanel> ChanelList;
        public DutyManager DutyManager;
        public DeviceManager Collection;
        public StatusChecker Pinger;
        private SshClient _sshConnector;
        private MySqlConnectionStringBuilder _connectionString;
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);


        public Device(string ipAddress, DeviceManager collection)
        {
            Message = "";
            Pinger = new StatusChecker(this);
            DutyManager = new DutyManager(this);
            ChanelList = new List<Chanel>();           
            IpAddress = ipAddress;
            Collection = collection;
            StatusPort = 10236;
            SqlIsBusy = false;

            _sshConnector = new SshClient(IpAddress, "pi", "raspberry");

            _connectionString = new MySqlConnectionStringBuilder
            {
                Server = IpAddress,
                UserID = "root",
                Password = "123",
                Database = "local_data_storage"
            };

            using (var connection = GetConnection())
            {
                SqlIsBusy = true;
                connection.Open();
                var chanels = connection.GetList<Settings>();

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, this));
                }

                connection.Close();
                SqlIsBusy = false;
            }

            foreach (var chanel in ChanelList)
            {
                var initThread = new Thread(() => chanel.Init());
                initThread.Start();
            }

            IsOnline = true;
            SyncSystemTime();
            Pinger.Start();
            DutyManager.Start();

            Thread.Sleep(100);
            DutyManager.SetCurrent(ChanelList[0], 1.0m);
            DutyManager.SetVoltage(ChanelList[0], 5.5m);
        }

        private void SyncSystemTime()
        {
            var date = RunSshCommand("date +%s");
            date = date.Remove(date.Length - 1);
            var time = Epoch.AddSeconds(Convert.ToInt64(date)).ToLocalTime();
         
            var diffinseconds = (DateTime.Now - time).TotalSeconds;
            if (diffinseconds > 5)
            {
                var reply = RunSshCommand(string.Concat("sudo date +%s -s @", (DateTime.Now - Epoch.ToLocalTime()).TotalSeconds));
                Console.WriteLine(reply);
            }
        }

        private string RunSshCommand(string command)
        {
            _sshConnector.Connect();
            var cmd = _sshConnector.CreateCommand(command);
            var result = cmd.Execute();
            _sshConnector.Disconnect();
            return result;
        }

        private void UpdateFromDatabase()
        {
            
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }
    }
}