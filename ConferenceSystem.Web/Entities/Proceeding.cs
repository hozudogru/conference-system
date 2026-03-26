using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class Proceeding
    {
        public long Id { get; set; }

        public long SubmissionId { get; set; }

        [StringLength(50)]
        public string? DOI { get; set; }

        public DateTime PublishedAt { get; set; } = DateTime.Now;

        public Submission? Submission { get; set; }
    }
}