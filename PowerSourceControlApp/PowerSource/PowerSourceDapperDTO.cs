using Dapper;

namespace PowerSourceControlApp
{
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
