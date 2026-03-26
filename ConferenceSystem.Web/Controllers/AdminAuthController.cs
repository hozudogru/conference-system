using ConferenceSystem.Web.Models;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConferenceSystem.Web.Controllers
{
    public class AdminAuthController : Controller
    {
        private readonly EditorAccountSettings _editorAccount;

        public AdminAuthController(IOptions<EditorAccountSettings> editorAccount)
        {
            _editorAccount = editorAccount.Value;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (HttpContext.Session.GetString("EditorAuthenticated") == "true")
            {
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View(new AdminLoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var configuredEmail = (_editorAccount.Email ?? string.Empty).Trim().ToLowerInvariant();
            var configuredPassword = _editorAccount.Password ?? string.Empty;
            var incomingEmail = (model.Email ?? string.Empty).Trim().ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(configuredEmail) || string.IsNullOrWhiteSpace(configuredPassword))
            {
                ModelState.AddModelError(string.Empty, "Editör hesabı henüz yapılandırılmamış. appsettings.json içindeki EditorAccount alanını doldurun.");
                return View(model);
            }

            if (incomingEmail != configuredEmail || model.Password != configuredPassword)
            {
                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
                return View(model);
            }

            HttpContext.Session.SetString("EditorAuthenticated", "true");
            HttpContext.Session.SetString("EditorEmail", _editorAccount.Email ?? "");
            HttpContext.Session.SetString("EditorDisplayName", _editorAccount.DisplayName ?? "Editor");

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("EditorAuthenticated");
            HttpContext.Session.Remove("EditorEmail");
            HttpContext.Session.Remove("EditorDisplayName");
            return RedirectToAction(nameof(Login));
        }
    }
}
