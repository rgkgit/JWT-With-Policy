namespace AutoSync.API.Helper
{
    public class Constants
    {
        public const string Error = "Internal server error. Please contact administrator.";
        public const string Invalid_User = "Invalid username or password";
        public const string User_Not_Exist = "User doesn't exist.";
        public const string Device_Id_Required = "DeviceId is required";
        public const string Device_Exist = "User already logged in another device";
        public const string Invalid_JobId = "JobId doesn't exist.";
        public const string Invalid_FileName = "Filename not found.";
        public const string No_Records = "No Data found";

        public enum JobStatus
        {
            Initiated,
            InProgress,
            Completed,
            Stopped
        }
    }
}
