using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DapperDTO;

namespace PowerSourceControlApp.PowerSource
{
    public sealed class Chanel : INotifyPropertyChanged
    {
        public PowerSource ParentPowerSource { get; }
        public List<Measurement> ResultsList;
        private readonly Random _randomNumberGenerator;
        public bool IsInited;

        public uint ChanelId { get; set; }
        public string ChanelUUID { get; set; }
        public uint Address { get; set; }
        public uint Status { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public decimal Power { get; set; }
        public bool Calibration { get; set; }
        [Magic]
        public bool OnOff { get; set; }

        private Settings _settings;

        public Chanel(uint chanelId, PowerSource parent)
        {
            ResultsList = new List<Measurement>();
            _randomNumberGenerator = new Random();
            ParentPowerSource = parent;
            ChanelId = chanelId;
            IsInited = false;
        }

        public void Init()
        {
            SyncWithTables();
        }

        private void SetSettingsTable()
        {
            _settings.Voltage = Voltage;
            _settings.Current = Current;
            _settings.Power = Power;
            _settings.Calibration = Calibration;
            _settings.OnOff = OnOff;

            using (var connection = new MySqlConnection(ParentPowerSource.MSQLConnectionString.ToString()))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                connection.Open();
                connection.UpdateAsync(_settings);
                connection.Close();
            }
        }

        private void SyncWithTables()
        {           
            var syncTableThread = new Thread(SyncThread)
            {
                Name = string.Concat("SyncSettingsAndResultsForPS:", ParentPowerSource.IpAddress, "Chanel:", ChanelId),
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };

            syncTableThread.Start();
        }
         
        private void SyncThread()
        {
            while (true)
            {
                Thread.Sleep(_randomNumberGenerator.Next(1000, 1100));

                if (ParentPowerSource.IsOnline)
                {
                    try
                    {
                        using (var connection = new MySqlConnection(ParentPowerSource.MSQLConnectionString.ToString()))
                        {
                            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                            connection.Open();

                            _settings = connection.Get<Settings>(ChanelId);
                            ResultsList = connection.GetList<Measurement>("where measured_at > date_sub(now(), interval 1 minute) ").ToList();

                            ChanelUUID = _settings.UUID;
                            Address = _settings.Address;
                            Voltage = _settings.Voltage;
                            Current = _settings.Current;
                            Power = _settings.Power;
                            Calibration = _settings.Calibration;
                            OnOff = _settings.OnOff;

                            connection.Close();

                            if (!IsInited)
                                IsInited = true;

                           /* if (ParentPowerSource.Collection.SelectedPowerSourceIp == ParentPowerSource.IpAddress)
                            {
                                ParentPowerSource.Collection.IsUpdated = true;
                            }*/
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                        //TODO: Need to implement some kind of SQL disconnect handler
                    }
                }
            }
        }

        private void RaisePropertyChanged(string propName)
        {
            if (IsInited)
            {
                if (ParentPowerSource.IsOnline)
                {
                    try
                    {
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

                        var updateThread = new Thread(SetSettingsTable)
                        {
                            IsBackground = true,
                            Priority = ThreadPriority.Normal
                        };
                        updateThread.Start();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
