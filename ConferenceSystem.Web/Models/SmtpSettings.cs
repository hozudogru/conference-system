namespace ConferenceSystem.Web.Models
{
    public class SmtpSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; } = 587;
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string FromEmail { get; set; } = "";
        public string FromName { get; set; } = "";
        public bool UseSsl { get; set; } = false;

        // Yeni alanlar
        public string SecurityMode { get; set; } = "Auto";
        public bool RequireAuthentication { get; set; } = true;
        public int TimeoutMilliseconds { get; set; } = 20000;
    }
}