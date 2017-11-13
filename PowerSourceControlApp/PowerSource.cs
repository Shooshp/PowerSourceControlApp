using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using System.Threading;

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

                CalibrationResult = connection.GetList<PowerSourceCalibration>(new {UUID = ChanelUUID}).ToList();
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

    class PowerSource
    {
        public string Server;
        public List<Chanel> ChanelList;

        public PowerSource(string server)
        {
            Server = server;
            ChanelList = new List<Chanel>();

            var connectionString = new MySqlConnectionStringBuilder
            {
                Server = Server,
                UserID = "root",
                Password = "123",
                Database = "local_data_storage"
            };

            using (var connection = GetConnection(connectionstring: connectionString))
            {
                connection.Open();
                var chanels = connection.GetList<PowerSourceSettings>();
                connection.Close();

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, connectionString));
                }
            }

            foreach (var chanel in ChanelList)
            {
                chanel.Init();
            }
        }

        private static MySqlConnection GetConnection(MySqlConnectionStringBuilder connectionstring)
        {
            var connection = new MySqlConnection(connectionstring.ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return connection;
        }
    }

    #region DTOClasses

    [Table("device_index")]
    public class DeviceIndex
    {
        [Key]
        [Column("device_type_id")]
        public string DeviceType { get; set; }
        [Column("address_prefix")]
        public uint AddressPrefix { get; set; }
    }

    [Table("devices_on_tester")]
    public class DevicesOnTester
    {
        [Key]
        [Column("devices_on_tester_uuid")]
        public string UUID { get; set; }
        [Column("devices_on_tester_type")]
        public string DeviceType { get; set; }
        [Column("devices_on_tester_add_at")]
        public string DeviceAddedAt { get; set; }
        [Column("devices_on_tester_address")]
        public uint Address { get; set; }
    }

    [Table("power_source_calibration")]
    public class PowerSourceCalibration
    {
        [Key]
        [Column("power_source_calibration_id")]
        public uint IndexId { get; set; }
        [Column("power_source_calibration_uuid")]
        public string UUID { get; set; }
        [Column("voltage_set")]
        public decimal VoltageSet { get; set; }
        [Column("voltage_get")]
        public decimal VoltageGet { get; set; }
    }

    [Table("power_source_settings")]
    public class PowerSourceSettings
    {
        [Key]
        [Column("power_source_settings_id")]
        public uint Id { get; set; }
        [Column("power_source_setting_uuid")]
        public string UUID { get; set; }
        [Column("power_source_settings_address")]
        public uint Address { get; set; }
        [Column("power_source_settings_voltage")]
        public decimal Voltage { get; set; }
        [Column("power_source_settings_current")]
        public decimal Current { get; set; }
        [Column("power_source_settings_power")]
        public decimal Power { get; set; }
        [Column("power_source_settings_calibration_set")]
        public uint Calibration { get; set; }
        [Column("power_source_settings_on_off")]
        public bool OnOff { get; set; }
        [Column("power_source_settings_status")]
        public uint Status { get; set; }
    }

    [Table("power_source_measurement")]
    public class PowerSourceMeasurement
    {
        [Key]
        [Column("power_source_measurement_id")]
        public uint IndexId { get; set; }
        [Column("power_source_measurement_uuid")]
        public string UUID { get; set; }
        [Column("measurement_voltage")]
        public decimal Voltage { get; set; }
        [Column("measurement_current")]
        public decimal Current { get; set; }
        [Column("measurement_temperature")]
        public decimal Temperature { get; set; }
        [Column("measured_at")]
        public string MeasuredAt { get; set; }
    }
    #endregion
}