using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
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
            }

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
                ["Theme.Primary"] = "#B08A4A",
                ["Theme.Secondary"] = "#F5E6C8",
                ["Theme.Accent"] = "#8D6A2B",
                ["Theme.Background"] = "#F7F3EC",
                ["Theme.Surface"] = "#FFFFFF",
                ["Theme.Border"] = "#E8D9BC",
                ["Theme.Heading"] = "#6F4E1F",
                ["Theme.Text"] = "#40352A",
                ["Theme.Muted"] = "#8C7A64",
                ["Theme.HeaderBg"] = "#F3EEDF",
                ["Theme.HeaderText"] = "#6F4E1F",
                ["Theme.FooterBg"] = "#F3EEDF",
                ["Theme.FooterText"] = "#6F4E1F",
                ["Theme.ButtonBg"] = "#B08A4A",
                ["Theme.ButtonText"] = "#FFFFFF",
                ["Theme.BadgeBg"] = "#F8EACD",
                ["Theme.BadgeText"] = "#8D6A2B"
            };

            var rawSettings = await _context.SiteSettings
                .Where(x => themeKeys.Contains(x.Key))
                .ToDictionaryAsync(x => x.Key, x => x.Value);

            var finalTheme = themeDefaults.ToDictionary(x => x.Key, x => rawSettings.ContainsKey(x.Key) && !string.IsNullOrWhiteSpace(rawSettings[x.Key]) ? rawSettings[x.Key] : x.Value);

            var model = new HomePageViewModel
            {
                ActiveConference = activeConference,
                Announcements = announcements,
                ImportantDates = importantDates,
                CommitteeMembers = committeeMembers,
                RegistrationCategories = registrationCategories,
                Speakers = speakers,
                HomePageSetting = homePageSetting,
                ThemeSettings = finalTheme
            };

            return View(model);
        }
    }
}
