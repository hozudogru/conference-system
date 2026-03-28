using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.ViewModels
{
    public class DecisionLetterTemplateViewModel
    {
        [Display(Name = "Mevcut Şablon Görseli")]
        public string? CurrentTemplateImagePath { get; set; }

        [Display(Name = "Mevcut İmza Görseli")]
        public string? CurrentSignatureImagePath { get; set; }

        [Display(Name = "Şablon Görseli")]
        public IFormFile? TemplateImage { get; set; }

        [Display(Name = "İmza Görseli")]
        public IFormFile? SignatureImage { get; set; }

        [Display(Name = "Kurul Başkanı Adı")]
        public string? ChairmanName { get; set; }

        [Display(Name = "Kurul Başkanı Ünvanı")]
        public string? ChairmanTitle { get; set; }

        public int PageWidthPx { get; set; } = 794;
        public int PageHeightPx { get; set; } = 1123;

        public string? FieldsJson { get; set; }

        public List<DecisionLetterFieldSetting> Fields { get; set; } = new();
    }
}
