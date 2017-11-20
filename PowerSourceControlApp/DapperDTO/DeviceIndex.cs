using Dapper;

namespace PowerSourceControlApp.DapperDTO
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

    #endregion
}
