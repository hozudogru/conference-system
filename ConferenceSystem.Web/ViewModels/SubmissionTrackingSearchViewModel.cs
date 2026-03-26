using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.ViewModels
{
    public class SubmissionTrackingSearchViewModel
    {
        [Required]
        public string SubmissionNumber { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}