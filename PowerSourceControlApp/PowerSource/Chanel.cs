using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public void Update()
        {
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
                }

                if (RecentCurrentDisplay != ResultsList[ResultsList.Count - 1].Current)
                {
                    RecentCurrentDisplay = ResultsList[ResultsList.Count - 1].Current;
                }
            }
            else
            {
                RecentVoltageDisplay = 0;
                RecentCurrentDisplay = 0;
            }

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
        }
    }
}
