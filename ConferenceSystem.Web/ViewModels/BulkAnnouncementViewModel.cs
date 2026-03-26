using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ConferenceSystem.Web.ViewModels
{
    public class BulkAnnouncementViewModel
    {
        [Required]
        public string RecipientType { get; set; } = "Authors";
        // Authors, Reviewers, Both, Selected, Excel

        public long? ConferenceId { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = "";

        [Required]
        public string MessageBody { get; set; } = "";

        // Test
        [EmailAddress]
        public string TestEmail { get; set; } = "";

        // Seçili alıcılar için virgüllü liste
        public string SelectedEmails { get; set; } = "";

        // Excel yükleme
        public IFormFile? ExcelFile { get; set; }
    }
}