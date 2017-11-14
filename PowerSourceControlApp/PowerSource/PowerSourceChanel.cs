using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;

namespace PowerSourceControlApp
{
    class MagicAttribute : Attribute { }
    class NoMagicAttribute : Attribute { }

    public class Chanel : INotifyPropertyChanged
    {
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

        public MySqlConnectionStringBuilder ConnectionString;
        public List<PowerSourceCalibration> CalibrationResult;
        public PowerSourceSettings Settings;

        public Chanel(uint chanelId, MySqlConnectionStringBuilder connectionString)
        {
            ChanelId = chanelId;
            ConnectionString = connectionString;
            _isInited = false;
        }

        public void Init()
        {
            using (var connection = GetConnection(connectionstring: ConnectionString))
            {
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
                _isInited = true;
            }
        }

        private void UpdateSettingsTable()
        {
            Settings.Voltage = Voltage;
            Settings.Current = Current;
            Settings.Power = Power;
            Settings.Calibration = Calibration;
            Settings.OnOff = OnOff;

            using (var connection = GetConnection(connectionstring: ConnectionString))
            {
                connection.Open();
                connection.UpdateAsync(Settings);
            }
        }

        protected virtual void RaisePropertyChanged(string propName)
        {
            if (_isInited)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
                Thread updateThread = new Thread(UpdateSettingsTable);
                updateThread.Start();
            }
        }

        private static MySqlConnection GetConnection(MySqlConnectionStringBuilder connectionstring)
        {
            var connection = new MySqlConnection(connectionstring.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
