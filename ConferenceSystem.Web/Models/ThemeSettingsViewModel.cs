using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Models
{
    public class ThemeSettingsViewModel
    {
        [Required] public string HeaderBackground { get; set; } = "#f8f6f1";
        [Required] public string HeaderText { get; set; } = "#44546a";
        [Required] public string HeaderAccent { get; set; } = "#c9a24b";
        [Required] public string HeaderHoverBackground { get; set; } = "#f3ead6";
        [Required] public string BodyBackground { get; set; } = "#f4f4f4";
        [Required] public string SurfaceBackground { get; set; } = "#ffffff";
        [Required] public string SurfaceBorder { get; set; } = "#e6dcc8";
        [Required] public string TitleColor { get; set; } = "#7b5a1e";
        [Required] public string TextColor { get; set; } = "#3f4d63";
        [Required] public string MutedTextColor { get; set; } = "#7f8a99";
        [Required] public string ButtonBackground { get; set; } = "#c9a24b";
        [Required] public string ButtonText { get; set; } = "#ffffff";
        [Required] public string FooterBackground { get; set; } = "#f8f6f1";
        [Required] public string FooterText { get; set; } = "#6b7280";

        public string BrandText { get; set; } = "MİLLETLERARASI TÜRK\nKOOPERATİFÇİLİK KONGRESİ";
        public string LoginButtonText { get; set; } = "Giriş Yap";
    }
}
