using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Dapper;

namespace PowerSourceControlApp
{
    class MagicAttribute : Attribute { }
    class NoMagicAttribute : Attribute { }

    public class Chanel : INotifyPropertyChanged
    {
        public PowerSource ParentPowerSource { get; }
        private bool _isInited;

        public uint ChanelId { get; set; }
        public string ChanelUUID { get; set; }
        public uint Address { get; set; }
        public uint Status { get; set; }
        [Magic]
        public decimal Voltage { get; set; }
        [Magic]
        public decimal Current { get; set; }
        [Magic]
        public decimal Power { get; set; }
        [Magic]
        public uint Calibration { get; set; }
        [Magic]
        public bool OnOff { get; set; }

        public List<PowerSourceCalibration> CalibrationResult;
        public PowerSourceSettings Settings;

        public Chanel(uint chanelId, PowerSource parent)
        {
            ParentPowerSource = parent;
            ChanelId = chanelId;
            _isInited = false;
        }

        public void Init()
        {
            if (ParentPowerSource.IsOnline)
            {
                try
                {
                    var name = string.Concat("GettingSettingsForPS:", ParentPowerSource.IpAddress, "Chanel:", ChanelId);

                    var initialSettingsReadThread = new Thread(GetSettingsTable)
                    {
                        Name = name,
                        IsBackground = true,
                        Priority = ThreadPriority.Normal
                    };

                    initialSettingsReadThread.Start();
                }
                catch (Exception)
                {
                }
            }

            GetSettingsTable();
            _isInited = true;
        }

        private void SetSettingsTable()
        {
            Settings.Voltage = Voltage;
            Settings.Current = Current;
            Settings.Power = Power;
            Settings.Calibration = Calibration;
            Settings.OnOff = OnOff;

            using (var connection = ParentPowerSource.GetConnection())
            {
                connection.Open();
                connection.UpdateAsync(Settings);
                connection.Close();
            }
        }

        private void GetSettingsTable()
        {
            using (var connection = ParentPowerSource.GetConnection())
            {
                connection.Open();

                Settings = connection.Get<PowerSourceSettings>(ChanelId);

                ChanelUUID = Settings.UUID;
                Address = Settings.Address;
                Voltage = Settings.Voltage;
                Current = Settings.Current;
                Power = Settings.Power;
                Calibration = Settings.Calibration;
                OnOff = Settings.OnOff;
                Status = Settings.Status;

                CalibrationResult = connection.GetList<PowerSourceCalibration>(new { UUID = ChanelUUID }).ToList();

                connection.Close();
            }
            if (!_isInited)
            {
                _isInited = true;
            }
        }

        protected virtual void RaisePropertyChanged(string propName)
        {
            if (_isInited)
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
