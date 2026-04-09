using ConferenceSystem.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselManagementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CarouselManagementController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(long? conferenceId)
        {
            var conference = conferenceId.HasValue
                ? await _context.Conferences.FirstOrDefaultAsync(x => x.Id == conferenceId.Value)
                : await _context.Conferences
                    .OrderByDescending(x => x.IsActive)
                    .ThenByDescending(x => x.StartDate)
                    .FirstOrDefaultAsync();

            if (conference == null)
                return RedirectToAction("Index", "Conferences");

            ViewBag.Conference = conference;
            ViewBag.CarouselImages = GetCarouselImageUrls(conference.Id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(long conferenceId, List<IFormFile>? carouselImages, List<string>? removeCarouselImages)
        {
            var conference = await _context.Conferences.FirstOrDefaultAsync(x => x.Id == conferenceId);
            if (conference == null)
                return NotFound();

            if (carouselImages != null && carouselImages.Any(x => x != null && x.Length > 0))
            {
                await SaveCarouselImagesAsync(conferenceId, carouselImages);
                TempData["Success"] = "Carousel görselleri güncellendi.";
            }

            if (removeCarouselImages != null && removeCarouselImages.Any())
            {
                RemoveCarouselImages(conferenceId, removeCarouselImages);
                TempData["Success"] = "Seçili carousel görselleri silindi.";
            }

            if ((carouselImages == null || !carouselImages.Any(x => x != null && x.Length > 0)) &&
                (removeCarouselImages == null || !removeCarouselImages.Any()))
            {
                TempData["Success"] = "Değişiklik yapılmadı.";
            }

            return RedirectToAction(nameof(Index), new { conferenceId });
        }

        private List<string> GetCarouselImageUrls(long conferenceId)
        {
            var folder = Path.Combine(_environment.WebRootPath, "uploads", "home-carousel", conferenceId.ToString());
            if (!Directory.Exists(folder))
                return new List<string>();

            return Directory.GetFiles(folder)
                .Where(IsSupportedImage)
                .Select(Path.GetFileName)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                .Select(x => $"/uploads/home-carousel/{conferenceId}/{x}")
                .ToList();
        }

        private async Task SaveCarouselImagesAsync(long conferenceId, List<IFormFile> carouselImages)
        {
            var folder = Path.Combine(_environment.WebRootPath, "uploads", "home-carousel", conferenceId.ToString());
            Directory.CreateDirectory(folder);

            foreach (var image in carouselImages.Where(x => x != null && x.Length > 0))
            {
                var extension = Path.GetExtension(image.FileName);
                if (!IsSupportedImage(extension))
                    continue;

                var safeName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{extension}";
                var fullPath = Path.Combine(folder, safeName);

                await using var stream = new FileStream(fullPath, FileMode.Create);
                await image.CopyToAsync(stream);
            }
        }

        private void RemoveCarouselImages(long conferenceId, List<string> removeCarouselImages)
        {
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
                    System.IO.File.Delete(fullPath);
            }
        }

        private static bool IsSupportedImage(string pathOrExtension)
        {
            var ext = Path.GetExtension(pathOrExtension);
            if (string.IsNullOrWhiteSpace(ext))
                ext = pathOrExtension;
            return ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                || ext.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
                || ext.Equals(".png", StringComparison.OrdinalIgnoreCase)
                || ext.Equals(".webp", StringComparison.OrdinalIgnoreCase)
                || ext.Equals(".gif", StringComparison.OrdinalIgnoreCase);
        }
    }
}
