using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class Review
    {
        public long Id { get; set; }

        [Required]
        public long ReviewAssignmentId { get; set; }

        public int OriginalityScore { get; set; }

        public int MethodScore { get; set; }

        public int RelevanceScore { get; set; }

        public int WritingScore { get; set; }

        public string CommentToAuthor { get; set; } = "";

        public string CommentToEditor { get; set; } = "";

        [StringLength(50)]
        public string Recommendation { get; set; } = "Revise";

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public ReviewAssignment? ReviewAssignment { get; set; }
    }
}