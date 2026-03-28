using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.ViewModels
{
    public class SmtpSettingsViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Host { get; set; } = "";

        [Range(1, 65535)]
        public int Port { get; set; } = 587;

        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        [Required]
        [EmailAddress]
        public string FromEmail { get; set; } = "";

        public string FromName { get; set; } = "";

        public bool UseSsl { get; set; } = false;

        [Required]
        public string SecurityMode { get; set; } = "Auto";

        public bool RequireAuthentication { get; set; } = true;

        [Range(1000, 120000)]
        public int TimeoutMilliseconds { get; set; } = 20000;

        [EmailAddress]
        public string? TestEmail { get; set; }

        public string DecisionTemplateImagePath { get; set; } = "";
        public string SignatureImagePath { get; set; } = "";
        public string ChairmanName { get; set; } = "";
        public string ChairmanTitle { get; set; } = "";
        public float LetterTopOffset { get; set; } = 110;
        public float LetterLeftOffset { get; set; } = 55;
        public float SignatureTopOffset { get; set; } = 30;

        public IFormFile? DecisionTemplateImageFile { get; set; }
        public IFormFile? SignatureImageFile { get; set; }
    }
}
