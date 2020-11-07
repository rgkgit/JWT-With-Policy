namespace AutoSync.API.Models
{
    public class JobCompletedModel
    {
        public string JobId { get; set; }
        public string FileName { get; set; }
        public string S3Url { get; set; }
    }
}
