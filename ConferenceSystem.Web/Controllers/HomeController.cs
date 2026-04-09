using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace ConferenceSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public HomeController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var activeConference = await _context.Conferences
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            var announcements = await _context.Announcements
                .Where(x => x.IsPublished)
                .OrderByDescending(x => x.PublishDate)
                .Take(6)
                .ToListAsync();

            var importantDates = new List<ConferenceSystem.Web.Entities.ImportantDate>();
            var committeeMembers = new List<ConferenceSystem.Web.Entities.CommitteeMember>();
            var registrationCategories = new List<ConferenceSystem.Web.Entities.RegistrationCategory>();
            var speakers = new List<ConferenceSystem.Web.Entities.Speaker>();
            var carouselImageUrls = new List<string>();
            ConferenceSystem.Web.Entities.HomePageSetting? homePageSetting = null;

            if (activeConference != null)
            {
                importantDates = await _context.ImportantDates
                    .Where(x => x.ConferenceId == activeConference.Id && x.IsActive)
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.DateValue)
                    .ToListAsync();

                committeeMembers = await _context.CommitteeMembers
                    .Where(x => x.ConferenceId == activeConference.Id && x.IsActive)
                    .OrderBy(x => x.CommitteeType)
                    .ThenBy(x => x.SortOrder)
                    .ThenBy(x => x.FullName)
                    .ToListAsync();

                registrationCategories = await _context.RegistrationCategories
                    .Where(x => x.ConferenceId == activeConference.Id && x.IsActive)
                    .OrderBy(x => x.SortOrder)
                    .ToListAsync();

                speakers = await _context.Speakers
                    .Where(x => x.ConferenceId == activeConference.Id && x.IsActive)
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.FullName)
                    .ToListAsync();

                homePageSetting = await _context.HomePageSettings
                    .FirstOrDefaultAsync(x => x.ConferenceId == activeConference.Id);

                var carouselFolder = Path.Combine(_environment.WebRootPath, "uploads", "home-carousel", activeConference.Id.ToString());
                if (Directory.Exists(carouselFolder))
                {
                    carouselImageUrls = Directory.GetFiles(carouselFolder)
                        .Where(path => IsSupportedImage(path))
                        .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                        .Select(path => $"/uploads/home-carousel/{activeConference.Id}/{Path.GetFileName(path)}")
                        .ToList();
                }
            }

            var menuItems = await _context.MenuItems
                .Where(x => x.IsActive && x.MenuLocation == "Public")
                .OrderBy(x => x.SortOrder)
                .ToListAsync();

            var themeKeys = new[]
            {
                "Theme.Primary",
                "Theme.Secondary",
                "Theme.Accent",
                "Theme.Background",
                "Theme.Surface",
                "Theme.Border",
                "Theme.Heading",
                "Theme.Text",
                "Theme.Muted",
                "Theme.HeaderBg",
                "Theme.HeaderText",
                "Theme.FooterBg",
                "Theme.FooterText",
                "Theme.ButtonBg",
                "Theme.ButtonText",
                "Theme.BadgeBg",
                "Theme.BadgeText"
            };

            var themeDefaults = new Dictionary<string, string>
            {
                ["Theme.Primary"] = "#7C5CFC",
                ["Theme.Secondary"] = "#D8CCFF",
                ["Theme.Accent"] = "#22D3EE",
                ["Theme.Background"] = "#0B1020",
                ["Theme.Surface"] = "rgba(255,255,255,0.08)",
                ["Theme.Border"] = "rgba(255,255,255,0.14)",
                ["Theme.Heading"] = "#F8FAFC",
                ["Theme.Text"] = "#D9E2F2",
                ["Theme.Muted"] = "#9FB0CC",
                ["Theme.HeaderBg"] = "rgba(11,16,32,0.75)",
                ["Theme.HeaderText"] = "#F8FAFC",
                ["Theme.FooterBg"] = "rgba(11,16,32,0.8)",
                ["Theme.FooterText"] = "#CBD5E1",
                ["Theme.ButtonBg"] = "#7C5CFC",
                ["Theme.ButtonText"] = "#FFFFFF",
                ["Theme.BadgeBg"] = "rgba(34,211,238,0.14)",
                ["Theme.BadgeText"] = "#67E8F9"
            };

            var rawSettings = await _context.SiteSettings
                .Where(x => themeKeys.Contains(x.Key))
                .ToDictionaryAsync(x => x.Key, x => x.Value);

            var finalTheme = themeDefaults.ToDictionary(x => x.Key, x => rawSettings.ContainsKey(x.Key) && !string.IsNullOrWhiteSpace(rawSettings[x.Key]) ? rawSettings[x.Key] : x.Value);

          
            if (activeConference != null)
            {
                var carouselFolder = Path.Combine(_environment.WebRootPath, "uploads", "home-carousel", activeConference.Id.ToString());
                if (Directory.Exists(carouselFolder))
                {
                    carouselImageUrls = Directory.GetFiles(carouselFolder)
                        .Select(Path.GetFileName)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                        .Select(x => $"/uploads/home-carousel/{activeConference.Id}/{x}")
                        .ToList();
                }
            }

            var model = new HomePageViewModel
            {
                ActiveConference = activeConference,
                Announcements = announcements,
                ImportantDates = importantDates,
                CommitteeMembers = committeeMembers,
                RegistrationCategories = registrationCategories,
                Speakers = speakers,
                HomePageSetting = homePageSetting,
                ThemeSettings = finalTheme,
                MenuItems = menuItems,
                CarouselImageUrls = carouselImageUrls
            };

            return View(model);
        }

        private static bool IsSupportedImage(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext is ".jpg" or ".jpeg" or ".png" or ".webp" or ".gif";
        }
    }
}
