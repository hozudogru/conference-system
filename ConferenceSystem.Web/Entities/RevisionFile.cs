using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class RevisionFile
    {
        public long Id { get; set; }

        [Required]
        public long SubmissionId { get; set; }

        [StringLength(300)]
        public string FilePath { get; set; } = "";

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public Submission? Submission { get; set; }
    }
}