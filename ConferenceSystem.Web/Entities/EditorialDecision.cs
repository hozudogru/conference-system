using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class EditorialDecision
    {
        public long Id { get; set; }

        [Required]
        public long SubmissionId { get; set; }

        [StringLength(50)]
        public string DecisionType { get; set; } = "Revision Required";

        public string DecisionNote { get; set; } = "";

        public DateTime DecidedAt { get; set; } = DateTime.Now;

        public Submission? Submission { get; set; }
    }
}