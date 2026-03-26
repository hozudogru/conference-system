using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class Reviewer
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = "";

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = "";

        [StringLength(250)]
        public string Expertise { get; set; } = "";

        public bool IsActive { get; set; } = true;
    }
}