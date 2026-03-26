using ConferenceSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceSystem.Web.Controllers
{
    public class TestMailController : Controller
    {
        private readonly EmailService _emailService;

        public TestMailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string toEmail)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
            {
                ViewBag.Result = "Lütfen e-posta adresi giriniz.";
                return View();
            }

            try
            {
                var result = await _emailService.SendSimpleEmailAsync(
                    toEmail.Trim(),
                    "Test User",
                    "SMTP Test Mail",
                    """
Bu bir test mailidir.

Eğer bu mesaj geldiyse:
- SMTP bağlantısı çalışıyor
- kimlik doğrulama çalışıyor
- sistem mail gönderebiliyor

Conference System
"""
                );

                ViewBag.Result = result;
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.ToString();
            }

            return View();
        }
    }
}