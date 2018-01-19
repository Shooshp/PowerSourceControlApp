using System;
using Dapper;

namespace PowerSourceControlApp.DapperDTO
{
    #region DTOClasses

    [Table("Logs")]
    public class Logs
    {
        [Key]
        public uint   Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        public string LogEvent { get; set; }
        public string User { get; set; }
        public string Host { get; set; }
    }

    #endregion
}
