namespace ConferenceSystem.Web.Entities
{
    public class SessionItem
    {
        public long Id { get; set; }

        public long SessionId { get; set; }
        public long SubmissionId { get; set; }

        public int Order { get; set; }
        public int PresentationMinutes { get; set; } = 15;

        public Session? Session { get; set; }
        public Submission? Submission { get; set; }
    }
}