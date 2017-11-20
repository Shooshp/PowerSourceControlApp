using Dapper;

namespace PowerSourceControlApp.DapperDTO
{
    #region DTOClasses

    [Table("power_source_measurement")]
    public class Measurement
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
