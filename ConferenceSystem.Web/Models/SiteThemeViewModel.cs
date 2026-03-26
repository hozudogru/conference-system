namespace ConferenceSystem.Web.Models
{
    public class SiteThemeViewModel
    {
        public string BrandText { get; set; } = "MİLLETLERARASI TÜRK\nKOOPERATİFÇİLİK KONGRESİ";
        public string BrandLogoUrl { get; set; } = "/images/logo.png";
        public string HeaderBackground { get; set; } = "#f7f4ee";
        public string HeaderTextColor { get; set; } = "#48566a";
        public string AccentColor { get; set; } = "#c59d4f";
        public string ButtonTextColor { get; set; } = "#ffffff";
        public string BodyBackground { get; set; } = "#f5f5f5";
        public string SurfaceColor { get; set; } = "#ffffff";
        public string BorderColor { get; set; } = "#e6dcc7";
        public string HeadingColor { get; set; } = "#7a5a20";
        public string TextColor { get; set; } = "#48566a";
        public string FooterBackground { get; set; } = "#f7f4ee";
        public string FooterTextColor { get; set; } = "#6b7280";
        public string LoginButtonText { get; set; } = "Giriş Yap";
        public string LoginButtonUrl { get; set; } = "/AdminAuth/Login";
    }
}
