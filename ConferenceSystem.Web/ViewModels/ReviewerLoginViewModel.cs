using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.ViewModels
{
    public class ReviewerLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}