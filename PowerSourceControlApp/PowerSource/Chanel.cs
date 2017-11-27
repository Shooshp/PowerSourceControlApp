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
    public sealed class Chanel 
    {
        public PowerSource ParentPowerSource { get; }
        public Settings SettingsOnChanel;
        public List<Measurement> ResultsList;
        private readonly Random _randomNumberGenerator;
        public bool IsInited;
        public bool IsActive;

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

        public void StartSyncThread()
        {
            IsActive = true;

            var syncTableThread = new Thread(SyncThread)
            {
                Name = string.Concat("SyncSettingsAndResultsForPS:", ParentPowerSource.IpAddress, "Chanel:", ChanelId),
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };


            syncTableThread.Start();
        }

        public void StopSyncThread()
        {
            IsActive = false;
            GC.Collect();
        }
         
        private void SyncThread()
        {
            while (IsActive)
            {
                Thread.Sleep(_randomNumberGenerator.Next(500, 600));

                if (!OnOff)
                {
                    Status = 0;
                }
                else
                {
                    if (Calibration)
                    {
                        Status = 3;
                    }
                    else
                    {
                        Status = 2;
                    }
                }

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
                                if (!OnOff)
                                {
                                    ResultsList[ResultsList.Count - 1].Voltage = 0;
                                    ResultsList[ResultsList.Count - 1].Current = 0;
                                }

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
                                SyncSettings();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (!ParentPowerSource.IsOnline)
                        {
                            IsActive = false;
                        }
                        else
                        {
                            Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        public void SyncSettings()
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
