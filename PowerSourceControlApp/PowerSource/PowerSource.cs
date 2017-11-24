using System;
using System.Collections.Generic;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DapperDTO;
using PowerSourceControlApp.DeviceManagment;
using Renci.SshNet;

namespace PowerSourceControlApp.PowerSource
{
    class MagicAttribute : Attribute { }

    public class PowerSource
    {
        public string IpAddress { get; }
        public string Hostname { get; set; }
        public string DisplayName { get; set; }
        public string Status;
        public string Message;
        public bool IsOnline { get; set; }
        public readonly int StatusPort;
        public readonly List<Chanel> ChanelList;
        public readonly TaskManager DutyManager;
        public readonly DeviceManager Collection;
        public readonly StatusChecker Pinger;
        private readonly SshClient _sshConnector;
        public readonly MySqlConnectionStringBuilder MsqlConnectionString;

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);


        public PowerSource(string ipAddress, DeviceManager collection)
        {
            Message = "";
            Hostname = "";
            DisplayName = "";
            Pinger = new StatusChecker(this);
            DutyManager = new TaskManager(this);
            ChanelList = new List<Chanel>();           
            IpAddress = ipAddress;
            Collection = collection;
            StatusPort = 10236;

            _sshConnector = new SshClient(IpAddress, "pi", "raspberry");

            MsqlConnectionString = new MySqlConnectionStringBuilder
            {
                Server = IpAddress,
                UserID = "root",
                Password = "123",
                Database = "local_data_storage"
            };

            IsOnline = true;
            GetHostname();
            Pinger.Start();
            GetChanelList();
            InitChanels();
            SyncSystemTime();
            DutyManager.Start();

            DutyManager.SetCurrent(ChanelList[0], 0.05m);
            DutyManager.SetVoltage(ChanelList[0], 5.5m);
        }

        private void GetChanelList()
        {
            using (var connection = new MySqlConnection(MsqlConnectionString.ToString()))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                connection.Open();
                var chanels = connection.GetList<Settings>();

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, this));
                }
                connection.Close();
            }
        }

        private void InitChanels()
        {
            foreach (var chanel in ChanelList)
            {
                var initThread = new Thread(() => chanel.Init());
                initThread.Start();
            }
            while (ChanelList.Exists(e => e.IsInited == false))
            {
                Thread.Sleep(1);
            }
        }

        private void GetHostname()
        {
            Hostname = RunSshCommand("hostname");
            DisplayName = string.Concat(Hostname, "(", IpAddress, ")");
        }

        private void SetHostname(string name)
        {
            RunSshCommand(string.Concat("sudo hostnamectl set-hostname ", name));
            GetHostname();
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
    }
}