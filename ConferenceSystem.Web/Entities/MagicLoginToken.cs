using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class MagicLoginToken
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; } = "";

        [StringLength(100)]
        public string Token { get; set; } = "";

        [StringLength(50)]
        public string UserType { get; set; } = ""; // Author / Reviewer

        public long? SubmissionId { get; set; }
        public long? ReviewerId { get; set; }

        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}