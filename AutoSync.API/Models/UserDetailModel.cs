using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSync.API.Models
{
    public class UserDetailModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public long SupervisorId { get; set; }
        public string FolderFilePath { get; set; }
        public string AutoSyncTime { get; set; }
        public string AutoSyncDays { get; set; }
        public int? AutoDeleteInterval { get; set; }
        public bool IsActive { get; set; }
    }
}
