using System.Collections.Generic;

namespace AutoSync.API.Models
{
    public class JobRequestModel
    {
        public string JobId { get; set; }
        public FileModel Reads { get; set; }
        public FileModel Bills { get; set; }
        public List<FileModel> Images { get; set; }
        public string Reason { get; set; }
        public bool IsManualSync { get; set; }
    }

    public class FileModel
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
}
