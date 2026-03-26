using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.ViewModels
{
    public class AccessLoginViewModel
    {
        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = "";

        [Display(Name = "Başvuru Numarası")]
        public string? SubmissionNumber { get; set; }
    }
}
