using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using ConferenceSystem.Web.Services;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public AccessController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login(string? email = null, string? submissionNumber = null)
        {
            return View(new AccessLoginViewModel
            {
                Email = email ?? string.Empty,
                SubmissionNumber = submissionNumber
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccessLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var normalizedEmail = model.Email.Trim().ToLowerInvariant();
            var normalizedSubmissionNumber = model.SubmissionNumber?.Trim();

            Submission? submission = null;
            Reviewer? reviewer = null;

            if (!string.IsNullOrWhiteSpace(normalizedSubmissionNumber))
            {
                submission = await _context.Submissions
                    .Include(x => x.Conference)
                    .FirstOrDefaultAsync(x =>
                        x.SubmissionNumber == normalizedSubmissionNumber &&
                        x.Email.ToLower() == normalizedEmail);
            }

            if (submission != null)
                return await SendAuthorMagicLink(model, submission);

            reviewer = await _context.Reviewers
                .FirstOrDefaultAsync(x => x.Email.ToLower() == normalizedEmail && x.IsActive);

            if (reviewer != null)
                return await SendReviewerMagicLink(model, reviewer);

            ModelState.AddModelError("", "Bu bilgilerle eşleşen aktif bir kayıt bulunamadı. Yazar girişi için başvuru numarasını da giriniz.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MagicLogin(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest("Geçersiz token.");

            var loginToken = await _context.MagicLoginTokens
                .FirstOrDefaultAsync(x =>
                    x.Token == token &&
                    !x.IsUsed &&
                    x.ExpiresAt > DateTime.Now);

            if (loginToken == null)
                return BadRequest("Link geçersiz veya süresi dolmuş.");

            loginToken.IsUsed = true;
            await _context.SaveChangesAsync();

            if (loginToken.UserType == "Author" && loginToken.SubmissionId.HasValue)
            {
                var submission = await _context.Submissions
                    .FirstOrDefaultAsync(x => x.Id == loginToken.SubmissionId.Value);

                if (submission == null)
                    return NotFound();

                HttpContext.Session.SetString("PortalRole", "Author");
                HttpContext.Session.SetString("AuthorEmail", submission.Email);
                HttpContext.Session.SetString("AuthorSubmissionId", submission.Id.ToString());

                return RedirectToAction("Detail", "AuthorPanel", new
                {
                    submissionId = submission.Id,
                    email = submission.Email
                });
            }

            if (loginToken.UserType == "Reviewer" && loginToken.ReviewerId.HasValue)
            {
                HttpContext.Session.SetString("PortalRole", "Reviewer");
                HttpContext.Session.SetString("ReviewerId", loginToken.ReviewerId.Value.ToString());
                HttpContext.Session.SetString("ReviewerEmail", loginToken.Email);

                return RedirectToAction("Assignments", "ReviewerPanel", new
                {
                    reviewerId = loginToken.ReviewerId.Value
                });
            }

            return BadRequest("Token rol bilgisi geçersiz.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("PortalRole");
            HttpContext.Session.Remove("AuthorEmail");
            HttpContext.Session.Remove("AuthorSubmissionId");
            HttpContext.Session.Remove("ReviewerId");
            HttpContext.Session.Remove("ReviewerEmail");
            return RedirectToAction(nameof(Login));
        }

        private async Task<IActionResult> SendAuthorMagicLink(AccessLoginViewModel model, Submission submission)
        {
            var tokenValue = Guid.NewGuid().ToString("N");

            var token = new MagicLoginToken
            {
                Email = submission.Email,
                Token = tokenValue,
                UserType = "Author",
                SubmissionId = submission.Id,
                ExpiresAt = DateTime.Now.AddMinutes(30),
                IsUsed = false,
                CreatedAt = DateTime.Now
            };

            _context.MagicLoginTokens.Add(token);
            await _context.SaveChangesAsync();

            var loginUrl = Url.Action("MagicLogin", "Access", new { token = tokenValue }, Request.Scheme);

            try
            {
                await _emailService.SendDecisionEmailAsync(
                    submission.Email,
                    submission.AuthorFullName,
                    "Yazar Giriş Linki",
                    $"""
Sayın {submission.AuthorFullName},

Yazar paneline aşağıdaki güvenli bağlantı ile giriş yapabilirsiniz:

{loginUrl}

Bu bağlantı 30 dakika geçerlidir.

Saygılarımızla,
Konferans Düzenleme Kurulu
""");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Magic link oluşturuldu ancak e-posta gönderilemedi: {ex.GetBaseException().Message}");
                return View("Login", model);
            }

            TempData["Success"] = "Yazar giriş linki e-posta adresinize gönderildi.";
            return RedirectToAction(nameof(Login));
        }

        private async Task<IActionResult> SendReviewerMagicLink(AccessLoginViewModel model, Reviewer reviewer)
        {
            var tokenValue = Guid.NewGuid().ToString("N");

            var token = new MagicLoginToken
            {
                Email = reviewer.Email,
                Token = tokenValue,
                UserType = "Reviewer",
                ReviewerId = reviewer.Id,
                ExpiresAt = DateTime.Now.AddMinutes(30),
                IsUsed = false,
                CreatedAt = DateTime.Now
            };

            _context.MagicLoginTokens.Add(token);
            await _context.SaveChangesAsync();

            var loginUrl = Url.Action("MagicLogin", "Access", new { token = tokenValue }, Request.Scheme);

            try
            {
                await _emailService.SendDecisionEmailAsync(
                    reviewer.Email,
                    reviewer.FullName,
                    "Hakem Giriş Linki",
                    $"""
Sayın {reviewer.FullName},

Hakem paneline aşağıdaki güvenli bağlantı ile giriş yapabilirsiniz:

{loginUrl}

Bu bağlantı 30 dakika geçerlidir.

Saygılarımızla,
Konferans Düzenleme Kurulu
""");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Magic link oluşturuldu ancak e-posta gönderilemedi: {ex.GetBaseException().Message}");
                return View("Login", model);
            }

            TempData["Success"] = "Hakem giriş linki e-posta adresinize gönderildi.";
            return RedirectToAction(nameof(Login));
        }
    }
}
