using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Dapper;
using PowerSourceControlApp.DapperDTO;

namespace PowerSourceControlApp.PowerSource
{
    class MagicAttribute : Attribute { }
    class NoMagicAttribute : Attribute { }

    public class Chanel : INotifyPropertyChanged
    {
        public Device ParentDevice { get; }
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
        public bool Calibration { get; set; }
        [Magic]
        public bool OnOff { get; set; }

        private List<Calibration> _calibrationResult;
        private Settings _settings;

        public Chanel(uint chanelId, Device parent)
        {
            ParentDevice = parent;
            ChanelId = chanelId;
            _isInited = false;
        }

        public void Init()
        {
            if (ParentDevice.IsOnline)
            {
                try
                {
                    var initialSettingsReadThread = new Thread(GetSettingsTable)
                    {
                        Name = string.Concat("GettingSettingsForPS:", ParentDevice.IpAddress, "Chanel:", ChanelId),
                        IsBackground = true,
                        Priority = ThreadPriority.Normal
                    };

                    initialSettingsReadThread.Start();
                    /*
                    var initialCalibrationReadThread = new Thread(GetCalibrationTable)
                    {
                        Name = string.Concat("GettingCalibrationForPS:", ParentDevice.IpAddress, "Chanel:",
                            ChanelId),
                        IsBackground = true,
                        Priority = ThreadPriority.Normal
                    };

                    initialCalibrationReadThread.Start();
                    */
                }
                catch (Exception)
                {
                }
            }

            _isInited = true;
        }

        private void SetSettingsTable()
        {
            _settings.Voltage = Voltage;
            _settings.Current = Current;
            _settings.Power = Power;
            _settings.Calibration = Calibration;
            _settings.OnOff = OnOff;

            using (var connection = ParentDevice.GetConnection())
            {
                connection.Open();
                connection.UpdateAsync(_settings);
                connection.Close();
            }
        }

        public void SyncWithSettingsTable()
        {
            if (ParentDevice.IsOnline)
            {
                try
                {
                    var syncSettingsTableThread = new Thread(GetSettingsTable)
                    {
                        Name = string.Concat("GettingSettingsForPS:", ParentDevice.IpAddress, "Chanel:", ChanelId),
                        IsBackground = true,
                        Priority = ThreadPriority.Normal
                    };

                    syncSettingsTableThread.Start();
                }
                catch (Exception)
                {
                }
            }
        }

        private void GetSettingsTable()
        {
            while (ParentDevice.SqlIsBusy)
            {
                Thread.Sleep(1);
            }
            ParentDevice.SqlIsBusy = true;
            using (var connection = ParentDevice.GetConnection())
            {
                connection.Open();

                _settings = connection.Get<Settings>(ChanelId);

                ChanelUUID = _settings.UUID;
                Address = _settings.Address;
                Voltage = _settings.Voltage;
                Current = _settings.Current;
                Power = _settings.Power;
                Calibration = _settings.Calibration;
                OnOff = _settings.OnOff;

                connection.Close();
            }
            ParentDevice.SqlIsBusy = false;
            if (!_isInited)
            {
                _isInited = true;
            }
        }

        private void GetCalibrationTable()
        {
            while (ParentDevice.SqlIsBusy)
            {
                Thread.Sleep(1);
            }
            ParentDevice.SqlIsBusy = true;
            using (var connection = ParentDevice.GetConnection())
            {
                connection.Open();
                _calibrationResult = connection.GetList<Calibration>(new { UUID = ChanelUUID }).ToList();
                connection.Close();
            }
            ParentDevice.SqlIsBusy = false;
        }

        protected virtual void RaisePropertyChanged(string propName)
        {
            if (_isInited)
            {
                if (ParentDevice.IsOnline)
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
