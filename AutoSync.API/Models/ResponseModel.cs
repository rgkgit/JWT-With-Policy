namespace AutoSync.API.Models
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
    }
}
