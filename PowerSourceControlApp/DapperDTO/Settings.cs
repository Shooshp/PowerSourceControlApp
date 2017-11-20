using Dapper;

namespace PowerSourceControlApp.DapperDTO
{
    #region DTOClasses

    [Table("power_source_settings")]
    public class Settings
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
        [Column("power_source_settings_calibration")]
        public bool Calibration { get; set; }
        [Column("power_source_settings_on_off")]
        public bool OnOff { get; set; }
    }

    #endregion
}
