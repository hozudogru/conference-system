using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class Submission
    {
        public long Id { get; set; }

        [Required]
        public long ConferenceId { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; } = "";

        [StringLength(300)]
        public string Keywords { get; set; } = "";

        [Required]
        public string AbstractText { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string AuthorFullName { get; set; } = "";

        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = "";

        [StringLength(250)]
        public string Institution { get; set; } = "";

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(300)]
        public string FilePath { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? RevisionReminderSentAt { get; set; }
        public Conference? Conference { get; set; }
        
        [StringLength(50)]
        public string SubmissionNumber { get; set; } = "";
    }
}