using ConferenceSystem.Web.Entities;

namespace ConferenceSystem.Web.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalSubmissions { get; set; }
        public int TotalReviewers { get; set; }
        public int TotalAssignments { get; set; }
        public int TotalReviews { get; set; }

        public int PendingSubmissions { get; set; }
        public int UnderReviewSubmissions { get; set; }
        public int ReviewedSubmissions { get; set; }
        public int AcceptedSubmissions { get; set; }
        public int RevisionRequiredSubmissions { get; set; }
        public int RejectedSubmissions { get; set; }
        public int RevisedSubmittedSubmissions { get; set; }

        public int TotalConferences { get; set; }
        public int TotalAnnouncements { get; set; }

        public List<Submission> LatestSubmissions { get; set; } = new();
        public List<EditorialDecision> LatestDecisions { get; set; } = new();
        public List<ReviewAssignment> LatestAssignments { get; set; } = new();
        public List<string> StatusLabels { get; set; } = new();
        public List<int> StatusCounts { get; set; } = new();
    }
}