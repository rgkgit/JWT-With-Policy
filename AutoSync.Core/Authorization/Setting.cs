using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoSync.Core.Authorization
{
    [Table("Setting")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string DeviceId { get; set; }
        public long SupervisorId { get; set; }
        public string FolderFilePath { get; set; }
        public string AutoSyncTime { get; set; }
        public string AutoSyncDays { get; set; }
        public int? AutoDeleteInterval { get; set; }
        public long? AuditDetailId { get; set; }
    }
}
