using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DapperDTO;
using PowerSourceControlApp.DeviceManagment;
using PowerSourceControlApp.PowerSource.Tasks;
using Renci.SshNet;

namespace PowerSourceControlApp.PowerSource
{
    class MagicAttribute : Attribute { }

    public class PowerSource : INotifyPropertyChanged
    {
        public string IpAddress { get; }
        public string Hostname { get; set; }
        public string DisplayName { get; set; }
        public string Status;
        public string Message;
        [Magic]
        public bool IsOnline { get; set; }

        private int _hash;
        private Thread _syncChanelsThread;


        public readonly int StatusPort;
        private List<Settings> _settingsList;
        private List<Measurement> _measurementList;
        public readonly List<Chanel> ChanelList;
        public readonly TaskManager DutyManager;
        public readonly StatusChecker Pinger;
        private readonly SshClient _sshConnector;
        public readonly MySqlConnectionStringBuilder MsqlConnectionString;

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);


        public PowerSource(string ipAddress)
        {
            Message = "";
            Hostname = "";
            DisplayName = "";
            Pinger = new StatusChecker(this);
            DutyManager = new TaskManager(this);
            ChanelList = new List<Chanel>();           
            IpAddress = ipAddress;
            StatusPort = 10236;

            _hash = 0;

            _settingsList = new List<Settings>();
            _measurementList = new List<Measurement>();

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
            SyncSystemTime();
            StartSyncChanelsThread();
        }

        private void GetChanelList()
        {
            try
            {
                using (var connection = new MySqlConnection(MsqlConnectionString.ToString()))
                {
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                    connection.Open();
                    _settingsList = connection.GetList<Settings>().ToList();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                if (IsOnline)
                {
                    Thread.Sleep(100);
                }
            }

            if (_settingsList.Count != 0)
            {
                foreach (var chanel in _settingsList)
                {
                    ChanelList.Add(new Chanel(chanel.Id, this));

                    ChanelList.Single(c => c.ChanelId == chanel.Id).ChanelUUID = chanel.UUID;
                    ChanelList.Single(c => c.ChanelId == chanel.Id).Address = chanel.Address;
                    ChanelList.Single(c => c.ChanelId == chanel.Id).Voltage = chanel.Voltage;
                    ChanelList.Single(c => c.ChanelId == chanel.Id).Current = chanel.Current;
                    ChanelList.Single(c => c.ChanelId == chanel.Id).Power = chanel.Power;
                    ChanelList.Single(c => c.ChanelId == chanel.Id).Calibration = chanel.Calibration;
                    ChanelList.Single(c => c.ChanelId == chanel.Id).OnOff = chanel.OnOff;
                }
            }
        }

        private void StartSyncChanelsThread()
        {
            if (_syncChanelsThread == null || _syncChanelsThread.ThreadState == ThreadState.Stopped)
            {
                var newThread = new Thread(SyncChanelsThread)
                {
                    IsBackground = true,
                    Name = string.Concat("SyncChanelsThread", IpAddress)
                };
                newThread.Start();
                Thread.MemoryBarrier();
                _syncChanelsThread = newThread;
            }
        }

        private void SyncChanelsThread()
        {
            while (IsOnline)
            {
                Thread.Sleep(800);

                try
                {
                    using (var connection = new MySqlConnection(MsqlConnectionString.ToString()))
                    {
                        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                        connection.Open();
                        _settingsList = connection.GetList<Settings>().ToList();
                        _measurementList = connection.GetList<Measurement>("where (measured_at > date_sub(now(), interval 1 minute))").ToList();
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                    if (IsOnline)
                    {
                        Thread.Sleep(100);
                    }
                }

                if (ChanelList.Count != 0)
                {
                    foreach (var chanel in _settingsList)
                    {
                        ChanelList.Single(c => c.ChanelId == chanel.Id).Voltage = chanel.Voltage;
                        ChanelList.Single(c => c.ChanelId == chanel.Id).Current = chanel.Current;
                        ChanelList.Single(c => c.ChanelId == chanel.Id).Power = chanel.Power;
                        ChanelList.Single(c => c.ChanelId == chanel.Id).Calibration = chanel.Calibration;
                        ChanelList.Single(c => c.ChanelId == chanel.Id).OnOff = chanel.OnOff;

                        ChanelList.Single(c => c.ChanelId == chanel.Id).ResultsList =
                             _measurementList.Where(a => a.UUID == chanel.UUID).ToList();

                        ChanelList.Single(c => c.ChanelId == chanel.Id).Update();
                    }
                }

                if (_hash != CheckHash())
                {
                    _hash = CheckHash();
                    DeviceManager.DeviceRefresh(IpAddress);
                }
            }
            _hash = 0;
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

        public void Update(decimal voltage, decimal current, uint chanelId)
        {
            var id = (int) chanelId;
            var index = ChanelList.FindIndex(a => a.ChanelId == chanelId);
            decimal voltageSet = ChanelList.Single(p => p.ChanelId == id).Voltage;
            decimal currentSet = ChanelList.Single(p => p.ChanelId == id).Current;

            if (voltageSet == 0) // Chanels voltage is zero
            {
                if (currentSet == 0) // Chanels current is zero
                {
                    if (current != 0) // But update current is not zero
                    {
                        DutyManager.SetCurrent(ChanelList[index], current);
                        if (voltage != 0) 
                        {
                            DutyManager.SetVoltage(ChanelList[index], voltage);
                        }
                    }
                    else
                    {
                        //TODO: Text Popoup with error: no voltage without current!
                    }
                }
                else // Chanel current is not zero
                {
                    if (current != 0)
                    {
                        if (currentSet != current) // Current not have same value
                        {
                            DutyManager.SetCurrent(ChanelList[index], current);
                        }
                        if (voltage != 0)
                        {
                            DutyManager.SetVoltage(ChanelList[index], voltage);
                        }
                    }
                    else
                    {
                        //TODO: Text Popoup with error: no voltage without current!
                    }
                }
            }
            else
            {
                if (voltage == 0)
                {
                    DutyManager.ShutDown(ChanelList[index]);
                }
                else
                {
                    if (current != 0)
                    {
                        if (currentSet != current)
                        {
                            DutyManager.SetCurrent(ChanelList[index], current);
                        }
                        if (voltageSet != voltage)
                        {
                            DutyManager.SetVoltage(ChanelList[index], voltage);
                        }
                    }
                    else
                    {
                        //TODO: Text Popoup with error: no voltage without current!
                    }
                }
            }
        }

        public void Switch(uint chanelId)
        {
            var index = ChanelList.FindIndex(a => a.ChanelId == chanelId);

            if (ChanelList[index].OnOff)
            {
                DutyManager.ShutDown(ChanelList[index]);
            }
            else
            {
                DutyManager.TurnOn(ChanelList[index]);
            }
        }

        private void RaisePropertyChanged(string propName)
        {
            if (propName == "IsOnline")
            {
                if (IsOnline)
                {
                    StartSyncChanelsThread();
                }
                else
                {
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int CheckHash()
        {
            var currentHash = 0;

            if (DutyManager.TaskList != null)
            {
                currentHash += DutyManager.TaskList.GetHashCode();
            }

            if (ChanelList != null)
            {
                currentHash += ChanelList.GetHashCode();
            }

            if (_settingsList != null)
            {
                currentHash += _settingsList.GetHashCode();
            }

            if (_measurementList != null)
            {
                currentHash += _measurementList.GetHashCode();
            }

            currentHash += IsOnline.GetHashCode();
            return currentHash;
        }
    }
}