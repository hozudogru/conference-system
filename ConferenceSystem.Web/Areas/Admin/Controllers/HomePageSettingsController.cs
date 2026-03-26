using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageSettingsController : Controller
    {
        private readonly AppDbContext _context;

        public HomePageSettingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? conferenceId)
        {
            var conference = conferenceId.HasValue
                ? await _context.Conferences.FirstOrDefaultAsync(x => x.Id == conferenceId.Value)
                : await _context.Conferences.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.StartDate).FirstOrDefaultAsync();

            if (conference == null)
                return RedirectToAction("Index", "Conferences");

            ViewBag.Conference = conference;

            var model = await _context.HomePageSettings.FirstOrDefaultAsync(x => x.ConferenceId == conference.Id)
                        ?? new HomePageSetting
                        {
                            ConferenceId = conference.Id,
                            HeroBadgeText = $"{conference.StartDate:dd MMMM yyyy} - {conference.EndDate:dd MMMM yyyy} • {conference.Location}",
                            HeroTitle = conference.Name,
                            HeroDescription = conference.Description,
                            HighlightBox2Value = $"{conference.StartDate:dd MMMM yyyy} - {conference.EndDate:dd MMMM yyyy}"
                        };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HomePageSetting model)
        {
            var conference = await _context.Conferences.FirstOrDefaultAsync(x => x.Id == model.ConferenceId);
            if (conference == null)
                return NotFound();

            ViewBag.Conference = conference;

            if (!ModelState.IsValid)
                return View(model);

            var existing = await _context.HomePageSettings.FirstOrDefaultAsync(x => x.ConferenceId == model.ConferenceId);
            if (existing == null)
            {
                _context.HomePageSettings.Add(model);
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(model);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Ana sayfa ayarları kaydedildi.";
            return RedirectToAction(nameof(Edit), new { conferenceId = model.ConferenceId });
        }
    }
}
