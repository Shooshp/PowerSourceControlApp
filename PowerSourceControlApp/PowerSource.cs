using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DevExpress.Data.Helpers;
using MySql.Data.MySqlClient;

namespace PowerSourceControlApp
{

    public class Chanel
    {
        public uint ChanelId { get; set; }
        public string ChanelUUID { get; set; }
        public uint Address { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public decimal Power { get; set; }
        public uint Calibration { get; set; }
        public bool OnOff { get; set; }
        public uint Status { get; set; }

        public MySqlConnection Connection;
        public List<PowerSourceCalibration> CalibrationResult;

        public Chanel(uint chanelId, MySqlConnection connection)
        {
            ChanelId = chanelId;
            Connection = connection;
        }

        public void Init()
        {
            using (Connection)
            {
                Connection.Open();
                var settings = Connection.Get<PowerSourceSettings>(ChanelId);

                ChanelUUID = settings.UUID;
                Address = settings.Address;
                Voltage = settings.Voltage;
                Current = settings.Current;
                Power = settings.Power;
                Calibration = settings.Calibration;
                OnOff = settings.OnOff;
                Status = settings.Status;

                CalibrationResult = Connection.GetList<PowerSourceCalibration>(new { UUID = ChanelUUID }).ToList();
            }
        }
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

                foreach (var chanel in chanels)
                {
                    ChanelList.Add(new Chanel(chanel.Id, connection));
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