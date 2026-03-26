namespace ConferenceSystem.Web.ViewModels
{
    public class SubmissionTrackingResultViewModel
    {
        public long SubmissionId { get; set; }
        public string ConferenceName { get; set; } = "";
        public string Title { get; set; } = "";
        public string AuthorFullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Institution { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public bool HasDecision { get; set; }
        public string DecisionType { get; set; } = "";
        public string DecisionNote { get; set; } = "";
    }
}