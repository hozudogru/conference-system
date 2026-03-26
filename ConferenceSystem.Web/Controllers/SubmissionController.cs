using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConferenceSystem.Web.Models;
using ConferenceSystem.Web.Services;
using Microsoft.Extensions.Options;

namespace ConferenceSystem.Web.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly EmailService _emailService;
        private readonly AdminNotificationSettings _adminNotificationSettings;
        private readonly AuditService _auditService;
        public SubmissionController(
     AppDbContext context,
     IWebHostEnvironment environment,
     EmailService emailService,
     Microsoft.Extensions.Options.IOptions<AdminNotificationSettings> adminNotificationSettings,
     AuditService auditService)
        {
            _context = context;
            _environment = environment;
            _emailService = emailService;
            _adminNotificationSettings = adminNotificationSettings.Value;
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadConferencesAsync();
            return View(new Submission());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Submission model, IFormFile? paperFile)
        {
            if (model.ConferenceId <= 0)
            {
                ModelState.AddModelError("ConferenceId", "Lütfen bir konferans seçiniz.");
            }

            if (!ModelState.IsValid)
            {
                await LoadConferencesAsync(model.ConferenceId);
                return View(model);
            }

            var existingSubmission = await _context.Submissions
                .FirstOrDefaultAsync(x => x.ConferenceId == model.ConferenceId && x.Email == model.Email);

            if (existingSubmission != null)
            {
                ModelState.AddModelError("", "Bu e-posta adresi ile bu konferans için daha önce bildiri gönderilmiştir.");
                await LoadConferencesAsync(model.ConferenceId);
                return View(model);
            }

            if (paperFile == null || paperFile.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bildirinizin dosyasını yükleyiniz.");
                await LoadConferencesAsync(model.ConferenceId);
                return View(model);
            }

            var extension = Path.GetExtension(paperFile.FileName).ToLower();
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Sadece PDF veya Word dosyası yükleyebilirsiniz.");
                await LoadConferencesAsync(model.ConferenceId);
                return View(model);
            }

            if (paperFile.Length > 10 * 1024 * 1024)
            {
                ModelState.AddModelError("", "Dosya boyutu 10MB'tan büyük olamaz.");
                await LoadConferencesAsync(model.ConferenceId);
                return View(model);
            }

            model.Status = "Pending";
            model.CreatedAt = DateTime.Now;

            _context.Submissions.Add(model);
            await _context.SaveChangesAsync();

            var conference = await _context.Conferences.FindAsync(model.ConferenceId);

            var prefix = "CONF";
            if (conference != null)
            {
                if (!string.IsNullOrWhiteSpace(conference.ShortName))
                    prefix = conference.ShortName;
                else if (conference.StartDate.Year > 1)
                    prefix = $"CONF{conference.StartDate.Year}";
            }

            model.SubmissionNumber = $"{prefix}-{model.Id:D3}";

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "submissions");
            Directory.CreateDirectory(uploadsFolder);

            var originalNameWithoutExtension = Path.GetFileNameWithoutExtension(paperFile.FileName);
            var cleanedOriginalName = CleanFileName(originalNameWithoutExtension);

            var fileName = $"{model.SubmissionNumber}_{cleanedOriginalName}{extension}";
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await paperFile.CopyToAsync(stream);
            }

            model.FilePath = $"/uploads/submissions/{fileName}";

            await _context.SaveChangesAsync();
            var conferenceName = conference?.Name ?? "Conference";

            try
            {
                await _emailService.SendSubmissionConfirmationEmailAsync(
                    model.Email,
                    model.AuthorFullName,
                    conferenceName,
                    model.Title,
                    model.SubmissionNumber
                );
            }
            catch
            {
                // İstersen sonra loglayabiliriz, şimdilik submission'ı bozmasın diye yutuyoruz
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(_adminNotificationSettings.EditorEmail))
                {
                    await _emailService.SendNewSubmissionAlertToEditorAsync(
                        _adminNotificationSettings.EditorEmail,
                        _adminNotificationSettings.EditorName,
                        conferenceName,
                        model.Title,
                        model.SubmissionNumber,
                        model.AuthorFullName,
                        model.Email
                    );
                }
            }
            catch
            {
                // İstersen sonra loglayabiliriz, şimdilik submission'ı bozmasın diye yutuyoruz
            }
            await _auditService.LogAsync(
                "Create",
                "Submission",
                model.Id,
                model.Email,
                $"Yeni bildiri oluşturuldu. SubmissionNumber={model.SubmissionNumber}, Title={model.Title}");
            TempData["Success"] = $"Bildiriniz başarıyla gönderildi. Başvuru Numaranız: {model.SubmissionNumber}";
            return RedirectToAction(nameof(Create));
        }

        private async Task LoadConferencesAsync(object? selectedValue = null)
        {
            var conferences = await _context.Conferences
                .OrderByDescending(x => x.IsActive)
                .ThenByDescending(x => x.StartDate)
                .ToListAsync();

            ViewBag.Conferences = new SelectList(conferences, "Id", "Name", selectedValue);
        }

        private string CleanFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();

            var cleaned = new string(fileName
                .Where(c => !invalidChars.Contains(c))
                .ToArray());

            cleaned = cleaned.Replace(" ", "_")
                             .Replace("ç", "c").Replace("Ç", "C")
                             .Replace("ğ", "g").Replace("Ğ", "G")
                             .Replace("ı", "i").Replace("İ", "I")
                             .Replace("ö", "o").Replace("Ö", "O")
                             .Replace("ş", "s").Replace("Ş", "S")
                             .Replace("ü", "u").Replace("Ü", "U");

            return cleaned;
        }
    }
}