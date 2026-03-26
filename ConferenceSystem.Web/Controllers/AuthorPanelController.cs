using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using ConferenceSystem.Web.Models;
using ConferenceSystem.Web.Services;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConferenceSystem.Web.Controllers
{
    public class AuthorPanelController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly EmailService _emailService;
        private readonly AuditService _auditService;

        public AuthorPanelController(
            AppDbContext context,
            IWebHostEnvironment environment,
            EmailService emailService,
            IOptions<AppSettings> appSettings,
            AuditService auditService)
        {
            _context = context;
            _environment = environment;
            _emailService = emailService;
            _auditService = auditService;
        }

        [HttpGet]
        public IActionResult Login() => RedirectToAction("Login", "Access");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AuthorLoginViewModel model) => RedirectToAction("Login", "Access", new { email = model.Email });

        [HttpGet]
        public IActionResult RequestMagicLink() => RedirectToAction("Login", "Access");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestMagicLink(AuthorMagicLinkRequestViewModel model) => RedirectToAction("Login", "Access", new
        {
            email = model.Email,
            submissionNumber = model.SubmissionNumber
        });

        [HttpGet]
        public IActionResult MagicLogin(string token) => RedirectToAction("MagicLogin", "Access", new { token });

        [HttpGet]
        public async Task<IActionResult> Detail(long submissionId, string email)
        {
            if (!CanAccessSubmission(submissionId, email))
                return RedirectToAction("Login", "Access");

            var submission = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == submissionId && x.Email == email);

            if (submission == null)
                return NotFound();

            ViewBag.LatestDecision = await _context.EditorialDecisions
                .Where(x => x.SubmissionId == submission.Id)
                .OrderByDescending(x => x.DecidedAt)
                .FirstOrDefaultAsync();

            ViewBag.Revisions = await _context.RevisionFiles
                .Where(x => x.SubmissionId == submission.Id)
                .OrderByDescending(x => x.UploadedAt)
                .ToListAsync();

            return View(submission);
        }

        [HttpGet]
        public async Task<IActionResult> UploadRevision(long submissionId, string email)
        {
            if (!CanAccessSubmission(submissionId, email))
                return RedirectToAction("Login", "Access");

            var submission = await _context.Submissions
                .FirstOrDefaultAsync(x => x.Id == submissionId && x.Email == email);

            if (submission == null)
                return NotFound();

            if (submission.Status != "Revision Required")
                return BadRequest("Bu başvuru için revizyon yükleme açık değil.");

            ViewBag.SubmissionId = submission.Id;
            ViewBag.Email = submission.Email;
            ViewBag.Title = submission.Title;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadRevision(long submissionId, string email, IFormFile? revisionFile)
        {
            if (!CanAccessSubmission(submissionId, email))
                return RedirectToAction("Login", "Access");

            var submission = await _context.Submissions
                .FirstOrDefaultAsync(x => x.Id == submissionId && x.Email == email);

            if (submission == null)
                return NotFound();

            if (submission.Status != "Revision Required")
                return BadRequest("Bu başvuru için revizyon yükleme açık değil.");

            if (revisionFile == null || revisionFile.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir dosya seçiniz.");
                ViewBag.SubmissionId = submission.Id;
                ViewBag.Email = submission.Email;
                ViewBag.Title = submission.Title;
                return View();
            }

            var extension = Path.GetExtension(revisionFile.FileName).ToLower();
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Sadece PDF veya Word dosyası yükleyebilirsiniz.");
                ViewBag.SubmissionId = submission.Id;
                ViewBag.Email = submission.Email;
                ViewBag.Title = submission.Title;
                return View();
            }

            if (revisionFile.Length > 10 * 1024 * 1024)
            {
                ModelState.AddModelError("", "Dosya boyutu 10MB'tan büyük olamaz.");
                ViewBag.SubmissionId = submission.Id;
                ViewBag.Email = submission.Email;
                ViewBag.Title = submission.Title;
                return View();
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "revisions");
            Directory.CreateDirectory(uploadsFolder);

            var originalNameWithoutExtension = Path.GetFileNameWithoutExtension(revisionFile.FileName);
            var cleanedOriginalName = CleanFileName(originalNameWithoutExtension);

            var fileName = $"{submission.SubmissionNumber}_REVISION_{cleanedOriginalName}{extension}";
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await revisionFile.CopyToAsync(stream);
            }

            var revision = new RevisionFile
            {
                SubmissionId = submission.Id,
                FilePath = $"/uploads/revisions/{fileName}",
                UploadedAt = DateTime.Now
            };

            _context.RevisionFiles.Add(revision);
            submission.Status = "Revision Uploaded";
            await _context.SaveChangesAsync();

            await _auditService.LogAsync(
                "UploadRevision",
                "Submission",
                submission.Id,
                submission.Email,
                $"Revizyon dosyası yüklendi. File={revision.FilePath}");

            TempData["Success"] = "Revizyon dosyanız başarıyla yüklendi.";
            return RedirectToAction(nameof(Detail), new { submissionId = submission.Id, email = submission.Email });
        }

        private bool CanAccessSubmission(long submissionId, string email)
        {
            var role = HttpContext.Session.GetString("PortalRole");
            var submissionIdText = HttpContext.Session.GetString("AuthorSubmissionId");
            var sessionEmail = HttpContext.Session.GetString("AuthorEmail");

            return role == "Author"
                && submissionIdText == submissionId.ToString()
                && string.Equals(sessionEmail, email, StringComparison.OrdinalIgnoreCase);
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
