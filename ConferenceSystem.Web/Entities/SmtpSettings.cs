namespace ConferenceSystem.Web.Entities
{
    public class SmtpSetting
    {
        public long Id { get; set; }

        public string Host { get; set; } = "";
        public int Port { get; set; } = 587;

        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public string FromEmail { get; set; } = "";
        public string FromName { get; set; } = "";

        public bool UseSsl { get; set; } = false;

        public string SecurityMode { get; set; } = "Auto";
        public bool RequireAuthentication { get; set; } = true;
        public int TimeoutMilliseconds { get; set; } = 20000;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}