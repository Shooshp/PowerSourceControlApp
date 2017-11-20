using Dapper;

namespace PowerSourceControlApp.DapperDTO
{
    #region DTOClasses

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

    #endregion
}
