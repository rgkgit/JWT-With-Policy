using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoSync.Core.Job
{
    [Table("Job")]
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string JobId { get; set; }
        public string SourceFilePath { get; set; }
        public string DestinationFilePath { get; set; }
        public string TotalFileSize { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string SyncType { get; set; }
    }
}
