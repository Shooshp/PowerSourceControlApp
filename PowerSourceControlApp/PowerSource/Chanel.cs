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

        public Settings SettingsOnChanel;

        public uint ChanelId { get; set; }
        public string ChanelUUID { get; set; }
        public uint Address { get; set; }
        public uint Status { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }

        public decimal RecentVoltageDisplay { get; set; }
        public decimal RecentCurrentDisplay { get; set; }

        public decimal Power { get; set; }
        public bool Calibration { get; set; }
        [Magic]
        public bool OnOff { get; set; }

        

        public Chanel(uint chanelId, PowerSource parent)
        {
            ResultsList = new List<Measurement>();
            _randomNumberGenerator = new Random();
            ParentPowerSource = parent;
            ChanelId = chanelId;
            IsInited = false;

            RecentVoltageDisplay = 0;
            RecentCurrentDisplay = 0;
        }

        public void Init()
        {
            SyncWithTables();
        }

        private void SetSettingsTable()
        {
            SettingsOnChanel.Voltage = Voltage;
            SettingsOnChanel.Current = Current;
            SettingsOnChanel.Power = Power;
            SettingsOnChanel.Calibration = Calibration;
            SettingsOnChanel.OnOff = OnOff;

            using (var connection = new MySqlConnection(ParentPowerSource.MsqlConnectionString.ToString()))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                connection.Open();
                connection.UpdateAsync(SettingsOnChanel);
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
                        using (var connection = new MySqlConnection(ParentPowerSource.MsqlConnectionString.ToString()))
                        {
                            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                            connection.Open();

                            SettingsOnChanel = connection.Get<Settings>(ChanelId);

                            while (SettingsOnChanel.UUID == null)
                            {
                                Thread.Sleep(1);
                            }
                                
                            ResultsList = connection.GetList<Measurement>(" where (measured_at > date_sub(now(), interval 1 minute)) AND ( power_source_measurement_uuid = @ThisUUID )", new { ThisUUID = SettingsOnChanel.UUID}).ToList();

                            if (ResultsList.Count != 0)
                            {
                                if (RecentVoltageDisplay != ResultsList[ResultsList.Count - 1].Voltage)
                                {
                                    RecentVoltageDisplay = ResultsList[ResultsList.Count - 1].Voltage;

                                    if (ParentPowerSource.Collection.SelectedPowerSourceIp ==
                                        ParentPowerSource.IpAddress)
                                    {
                                        ParentPowerSource.Collection.IsUpdated = true;
                                    }
                                }
                                if (RecentCurrentDisplay != ResultsList[ResultsList.Count - 1].Current)
                                {
                                    RecentCurrentDisplay = ResultsList[ResultsList.Count - 1].Current;

                                    if (ParentPowerSource.Collection.SelectedPowerSourceIp ==
                                        ParentPowerSource.IpAddress)
                                    {
                                        ParentPowerSource.Collection.IsUpdated = true;
                                    }
                                }
                            } 
                            
                            connection.Close();

                            if (!IsInited)
                            {
                                ChanelUUID = SettingsOnChanel.UUID;
                                Address = SettingsOnChanel.Address;
                                Voltage = SettingsOnChanel.Voltage;
                                Current = SettingsOnChanel.Current;
                                Power = SettingsOnChanel.Power;
                                Calibration = SettingsOnChanel.Calibration;
                                OnOff = SettingsOnChanel.OnOff;
                                IsInited = true;
                            }                             
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
