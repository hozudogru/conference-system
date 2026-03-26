using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class ReviewAssignment
    {
        public long Id { get; set; }

        [Required]
        public long SubmissionId { get; set; }

        [Required]
        public long ReviewerId { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; } = "Assigned";
        public DateTime? ReminderSentAt { get; set; }
        public Submission? Submission { get; set; }
        public Reviewer? Reviewer { get; set; }
        
    }
}