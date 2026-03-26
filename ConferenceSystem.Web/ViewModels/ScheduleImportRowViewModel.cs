namespace ConferenceSystem.Web.ViewModels
{
    public class ScheduleImportRowViewModel
    {
        public string SessionTitle { get; set; } = "";
        public string Room { get; set; } = "";
        public string ChairName { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SubmissionNumber { get; set; } = "";
        public int Order { get; set; }
        public int PresentationMinutes { get; set; } = 15;
    }
}