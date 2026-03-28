using ConferenceSystem.Web.Entities;
using ConferenceSystem.Web.Services;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SystemSettingsController : Controller
    {
        private readonly SmtpSettingsService _smtpSettingsService;
        private readonly EmailService _emailService;
        private readonly SiteSettingsService _siteSettingsService;
        private readonly IWebHostEnvironment _environment;

        public SystemSettingsController(
            SmtpSettingsService smtpSettingsService,
            EmailService emailService,
            SiteSettingsService siteSettingsService,
            IWebHostEnvironment environment)
        {
            _smtpSettingsService = smtpSettingsService;
            _emailService = emailService;
            _siteSettingsService = siteSettingsService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Smtp()
        {
            var entity = await _smtpSettingsService.GetOrCreateAsync();

            var model = new SmtpSettingsViewModel
            {
                Id = entity.Id,
                Host = entity.Host,
                Port = entity.Port,
                UserName = entity.UserName,
                Password = entity.Password,
                FromEmail = entity.FromEmail,
                FromName = entity.FromName,
                UseSsl = entity.UseSsl,
                SecurityMode = entity.SecurityMode,
                RequireAuthentication = entity.RequireAuthentication,
                TimeoutMilliseconds = entity.TimeoutMilliseconds,
                DecisionTemplateImagePath = await _siteSettingsService.GetValueAsync("Letter.TemplateImagePath"),
                SignatureImagePath = await _siteSettingsService.GetValueAsync("Letter.SignatureImagePath"),
                ChairmanName = await _siteSettingsService.GetValueAsync("Letter.ChairmanName"),
                ChairmanTitle = await _siteSettingsService.GetValueAsync("Letter.ChairmanTitle"),
                LetterTopOffset = ParseFloat(await _siteSettingsService.GetValueAsync("Letter.TopOffset", "110"), 110),
                LetterLeftOffset = ParseFloat(await _siteSettingsService.GetValueAsync("Letter.LeftOffset", "55"), 55),
                SignatureTopOffset = ParseFloat(await _siteSettingsService.GetValueAsync("Letter.SignatureTopOffset", "30"), 30)
            };

            LoadSecurityModes(model.SecurityMode);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Smtp(SmtpSettingsViewModel model)
        {
            LoadSecurityModes(model.SecurityMode);

            if (!ModelState.IsValid)
                return View(model);

            var entity = new SmtpSetting
            {
                Id = model.Id,
                Host = model.Host.Trim(),
                Port = model.Port,
                UserName = model.UserName?.Trim() ?? "",
                Password = model.Password ?? "",
                FromEmail = model.FromEmail.Trim(),
                FromName = model.FromName?.Trim() ?? "",
                UseSsl = model.UseSsl,
                SecurityMode = model.SecurityMode,
                RequireAuthentication = model.RequireAuthentication,
                TimeoutMilliseconds = model.TimeoutMilliseconds,
                UpdatedAt = DateTime.Now
            };

            await _smtpSettingsService.SaveAsync(entity);

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "letters");
            Directory.CreateDirectory(uploadsFolder);

            if (model.DecisionTemplateImageFile != null && model.DecisionTemplateImageFile.Length > 0)
            {
                var ext = Path.GetExtension(model.DecisionTemplateImageFile.FileName);
                var templateFileName = $"decision-template{ext}";
                var templatePath = Path.Combine(uploadsFolder, templateFileName);
                using var stream = new FileStream(templatePath, FileMode.Create);
                await model.DecisionTemplateImageFile.CopyToAsync(stream);
                model.DecisionTemplateImagePath = $"/uploads/letters/{templateFileName}";
            }

            if (model.SignatureImageFile != null && model.SignatureImageFile.Length > 0)
            {
                var ext = Path.GetExtension(model.SignatureImageFile.FileName);
                var signFileName = $"decision-signature{ext}";
                var signPath = Path.Combine(uploadsFolder, signFileName);
                using var stream = new FileStream(signPath, FileMode.Create);
                await model.SignatureImageFile.CopyToAsync(stream);
                model.SignatureImagePath = $"/uploads/letters/{signFileName}";
            }

            await _siteSettingsService.SetValueAsync("Letter.TemplateImagePath", model.DecisionTemplateImagePath ?? "");
            await _siteSettingsService.SetValueAsync("Letter.SignatureImagePath", model.SignatureImagePath ?? "");
            await _siteSettingsService.SetValueAsync("Letter.ChairmanName", model.ChairmanName ?? "");
            await _siteSettingsService.SetValueAsync("Letter.ChairmanTitle", model.ChairmanTitle ?? "");
            await _siteSettingsService.SetValueAsync("Letter.TopOffset", model.LetterTopOffset.ToString(System.Globalization.CultureInfo.InvariantCulture));
            await _siteSettingsService.SetValueAsync("Letter.LeftOffset", model.LetterLeftOffset.ToString(System.Globalization.CultureInfo.InvariantCulture));
            await _siteSettingsService.SetValueAsync("Letter.SignatureTopOffset", model.SignatureTopOffset.ToString(System.Globalization.CultureInfo.InvariantCulture));

            TempData["Success"] = "SMTP ve karar mektubu ayarları kaydedildi.";
            return RedirectToAction(nameof(Smtp));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendTestMail(SmtpSettingsViewModel model)
        {
            LoadSecurityModes(model.SecurityMode);

            if (string.IsNullOrWhiteSpace(model.TestEmail))
            {
                ModelState.AddModelError("", "Lütfen test e-posta adresi giriniz.");
                return View("Smtp", model);
            }

            if (!ModelState.IsValid)
                return View("Smtp", model);

            var entity = new SmtpSetting
            {
                Id = model.Id,
                Host = model.Host.Trim(),
                Port = model.Port,
                UserName = model.UserName?.Trim() ?? "",
                Password = model.Password ?? "",
                FromEmail = model.FromEmail.Trim(),
                FromName = model.FromName?.Trim() ?? "",
                UseSsl = model.UseSsl,
                SecurityMode = model.SecurityMode,
                RequireAuthentication = model.RequireAuthentication,
                TimeoutMilliseconds = model.TimeoutMilliseconds,
                UpdatedAt = DateTime.Now
            };

            await _smtpSettingsService.SaveAsync(entity);

            try
            {
                var log = await _emailService.SendSimpleEmailAsync(
                    model.TestEmail.Trim(),
                    "Test User",
                    "SMTP Test Mail",
                    """
Bu bir test mailidir.

Eğer bu mail geldiyse SMTP ayarları çalışıyor demektir.

Conference System
""");

                TempData["Success"] = $"Test mail gönderildi: {model.TestEmail}";
                TempData["MailLog"] = log;

                return RedirectToAction(nameof(Smtp));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Test mail gönderilemedi: {ex.Message}");
                return View("Smtp", model);
            }
        }

        private void LoadSecurityModes(string? selectedValue)
        {
            ViewBag.SecurityModes = new SelectList(new[]
            {
                new { Value = "Auto", Text = "Auto" },
                new { Value = "SslOnConnect", Text = "SslOnConnect" },
                new { Value = "StartTls", Text = "StartTls" },
                new { Value = "None", Text = "None" }
            }, "Value", "Text", selectedValue);
        }

        private static float ParseFloat(string? value, float defaultValue)
        {
            return float.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result)
                ? result
                : defaultValue;
        }
    }
}
