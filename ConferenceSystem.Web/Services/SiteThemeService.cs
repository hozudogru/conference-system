using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Services
{
    public class SiteThemeService
    {
        private readonly AppDbContext _context;

        public SiteThemeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SiteThemeViewModel> GetThemeAsync()
        {
            var settings = await _context.SiteSettings.AsNoTracking().ToListAsync();

            string Get(string key, string fallback)
                => settings.FirstOrDefault(x => x.Key == key)?.Value ?? fallback;

            return new SiteThemeViewModel
            {
                BrandText = Get("Theme.BrandText", "MİLLETLERARASI TÜRK\nKOOPERATİFÇİLİK KONGRESİ"),
                BrandLogoUrl = Get("Theme.BrandLogoUrl", "/images/logo.png"),
                HeaderBackground = Get("Theme.HeaderBackground", "#f7f4ee"),
                HeaderTextColor = Get("Theme.HeaderTextColor", "#48566a"),
                AccentColor = Get("Theme.AccentColor", "#c59d4f"),
                ButtonTextColor = Get("Theme.ButtonTextColor", "#ffffff"),
                BodyBackground = Get("Theme.BodyBackground", "#f5f5f5"),
                SurfaceColor = Get("Theme.SurfaceColor", "#ffffff"),
                BorderColor = Get("Theme.BorderColor", "#e6dcc7"),
                HeadingColor = Get("Theme.HeadingColor", "#7a5a20"),
                TextColor = Get("Theme.TextColor", "#48566a"),
                FooterBackground = Get("Theme.FooterBackground", "#f7f4ee"),
                FooterTextColor = Get("Theme.FooterTextColor", "#6b7280"),
                LoginButtonText = Get("Theme.LoginButtonText", "Giriş Yap"),
                LoginButtonUrl = Get("Theme.LoginButtonUrl", "/AdminAuth/Login")
            };
        }
    }
}
