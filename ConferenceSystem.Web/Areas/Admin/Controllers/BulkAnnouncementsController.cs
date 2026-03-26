using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BulkAnnouncementsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public BulkAnnouncementsController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendBulk(string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                TempData["Error"] = "Konu boş olamaz.";
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                TempData["Error"] = "Mesaj boş olamaz.";
                return RedirectToAction(nameof(Index));
            }

            var recipients = await _context.Submissions
                .Where(x => x.Email != null && x.Email != "")
                .Select(x => new
                {
                    Email = x.Email!,
                    Name = string.IsNullOrWhiteSpace(x.AuthorFullName) ? "User" : x.AuthorFullName
                })
                .Distinct()
                .ToListAsync();

            if (!recipients.Any())
            {
                TempData["Error"] = "Gönderilecek alıcı bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            int successCount = 0;
            int failCount = 0;

            foreach (var user in recipients)
            {
                try
                {
                    await _emailService.SendSimpleEmailAsync(
                        user.Email,
                        user.Name,
                        subject,
                        message);

                    successCount++;
                }
                catch
                {
                    failCount++;
                }
            }

            TempData["Success"] = $"Toplu mail tamamlandı. Başarılı: {successCount}, Hatalı: {failCount}";
            return RedirectToAction(nameof(Index));
        }
    }
}