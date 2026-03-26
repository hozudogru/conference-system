using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.ViewModels
{
    public class AuthorLoginViewModel
    {
        [Required]
        public long SubmissionId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}