using Dapper;

namespace PowerSourceControlApp.DapperDTO
{
    #region DTOClasses

    [Table("power_source_calibration")]
    public class Calibration
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

    #endregion
}
