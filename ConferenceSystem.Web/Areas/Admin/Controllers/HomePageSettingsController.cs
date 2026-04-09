using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageSettingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public HomePageSettingsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
            ViewBag.CurrentCarouselImages = GetCarouselImageUrls(conference.Id);

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
        public async Task<IActionResult> Edit(HomePageSetting model, List<IFormFile>? carouselImages, List<string>? removeCarouselImages)
        {
            var conference = await _context.Conferences.FirstOrDefaultAsync(x => x.Id == model.ConferenceId);
            if (conference == null)
                return NotFound();

            ViewBag.Conference = conference;

            if (!ModelState.IsValid)
            {
                ViewBag.CurrentCarouselImages = GetCarouselImageUrls(conference.Id);
                return View(model);
            }

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

            await SaveCarouselImagesAsync(conference.Id, carouselImages);
            RemoveCarouselImages(conference.Id, removeCarouselImages);

            TempData["Success"] = "Ana sayfa ayarları kaydedildi.";
            return RedirectToAction(nameof(Edit), new { conferenceId = model.ConferenceId });
        }

        private List<string> GetCarouselImageUrls(long conferenceId)
        {
            var folder = Path.Combine(_environment.WebRootPath, "uploads", "home-carousel", conferenceId.ToString());
            if (!Directory.Exists(folder))
                return new List<string>();

            return Directory.GetFiles(folder)
                .Select(Path.GetFileName)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                .Select(x => $"/uploads/home-carousel/{conferenceId}/{x}")
                .ToList();
        }

        private async Task SaveCarouselImagesAsync(long conferenceId, List<IFormFile>? carouselImages)
        {
            if (carouselImages == null || carouselImages.Count == 0)
                return;

            var folder = Path.Combine(_environment.WebRootPath, "uploads", "home-carousel", conferenceId.ToString());
            Directory.CreateDirectory(folder);

            foreach (var image in carouselImages.Where(x => x != null && x.Length > 0))
            {
                var extension = Path.GetExtension(image.FileName);
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
                if (!allowed.Contains(extension, StringComparer.OrdinalIgnoreCase))
                    continue;

                var safeName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{extension}";
                var fullPath = Path.Combine(folder, safeName);

                await using var stream = new FileStream(fullPath, FileMode.Create);
                await image.CopyToAsync(stream);
            }
        }

        private void RemoveCarouselImages(long conferenceId, List<string>? removeCarouselImages)
        {
            if (removeCarouselImages == null || removeCarouselImages.Count == 0)
                return;

            var folder = Path.Combine(_environment.WebRootPath, "uploads", "home-carousel", conferenceId.ToString());
            if (!Directory.Exists(folder))
                return;

            foreach (var item in removeCarouselImages.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var fileName = Path.GetFileName(item);
                if (string.IsNullOrWhiteSpace(fileName))
                    continue;

                var fullPath = Path.Combine(folder, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
        }
    }
}
