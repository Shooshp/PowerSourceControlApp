using Dapper;

namespace PowerSourceControlApp.DapperDTO
{
    #region DTOClasses

    [Table("power_source_current_tasks")]
    public class CurrentTasks
    {
        [Key]
        [Column("power_source_current_task_id")]
        public uint IndexId { get; set; }
        [Column("power_source_current_task_device_uuid")]
        public string UUID { get; set; }
        [Column("power_source_current_task_name")]
        public string TaskName { get; set; }
        [Column("power_source_current_task_completed")]
        public bool IsComplited { get; set; }
        [Column("power_source_current_task_begin_at")]
        public string BeginAt { get; set; }
    }
    #endregion
}
