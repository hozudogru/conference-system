using ConferenceSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThemeSettingsController : Controller
    {
        private readonly SiteSettingsService _siteSettingsService;

        private static readonly (string Key, string Label, string DefaultValue)[] ThemeFields =
        {
            ("Theme.Primary", "Ana Renk", "#B08A4A"),
            ("Theme.Secondary", "İkincil Renk", "#F5E6C8"),
            ("Theme.Accent", "Vurgu Rengi", "#8D6A2B"),
            ("Theme.Background", "Sayfa Arka Planı", "#F7F3EC"),
            ("Theme.Surface", "Kart Arka Planı", "#FFFFFF"),
            ("Theme.Border", "Kenarlık", "#E8D9BC"),
            ("Theme.Heading", "Başlık Rengi", "#6F4E1F"),
            ("Theme.Text", "Metin Rengi", "#40352A"),
            ("Theme.Muted", "İkincil Metin", "#8C7A64"),
            ("Theme.HeaderBg", "Üst Menü Arka Plan", "#F3EEDF"),
            ("Theme.HeaderText", "Üst Menü Yazı", "#6F4E1F"),
            ("Theme.FooterBg", "Alt Bilgi Arka Plan", "#F3EEDF"),
            ("Theme.FooterText", "Alt Bilgi Yazı", "#6F4E1F"),
            ("Theme.ButtonBg", "Buton Arka Plan", "#B08A4A"),
            ("Theme.ButtonText", "Buton Yazı", "#FFFFFF"),
            ("Theme.BadgeBg", "Hero Etiket Arka Plan", "#F8EACD"),
            ("Theme.BadgeText", "Hero Etiket Yazı", "#8D6A2B")
        };

        public ThemeSettingsController(SiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new Dictionary<string, string>();
            foreach (var field in ThemeFields)
            {
                model[field.Key] = await _siteSettingsService.GetValueAsync(field.Key, field.DefaultValue);
            }

            ViewBag.ThemeFields = ThemeFields;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            foreach (var field in ThemeFields)
            {
                var value = form[field.Key].ToString();
                await _siteSettingsService.SetValueAsync(field.Key, string.IsNullOrWhiteSpace(value) ? field.DefaultValue : value);
            }

            TempData["Success"] = "Tema ayarları kaydedildi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
