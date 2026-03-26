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

        public SystemSettingsController(
            SmtpSettingsService smtpSettingsService,
            EmailService emailService)
        {
            _smtpSettingsService = smtpSettingsService;
            _emailService = emailService;
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
                TimeoutMilliseconds = entity.TimeoutMilliseconds
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

            TempData["Success"] = "SMTP ayarları kaydedildi.";
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
    }
}