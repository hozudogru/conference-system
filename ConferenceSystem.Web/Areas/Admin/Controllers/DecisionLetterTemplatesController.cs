using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DecisionLetterTemplatesController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public DecisionLetterTemplatesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var settings = LoadExistingSettings();

            var model = new DecisionLetterTemplateViewModel
            {
                CurrentTemplateImagePath = settings?.CurrentTemplateImagePath,
                CurrentSignatureImagePath = settings?.CurrentSignatureImagePath,
                ChairmanName = settings?.ChairmanName,
                ChairmanTitle = settings?.ChairmanTitle,
                PageWidthPx = settings?.PageWidthPx ?? 794,
                PageHeightPx = settings?.PageHeightPx ?? 1123,
                Fields = settings?.Fields?.Any() == true ? settings.Fields : GetDefaultFields()
            };

            model.FieldsJson = JsonSerializer.Serialize(model.Fields, _jsonOptions);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DecisionLetterTemplateViewModel model)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", "letters");
            Directory.CreateDirectory(uploadFolder);

            var existing = LoadExistingSettings();

            string? templatePath = existing?.CurrentTemplateImagePath;
            string? signaturePath = existing?.CurrentSignatureImagePath;

            if (model.TemplateImage != null && model.TemplateImage.Length > 0)
            {
                var templateFileName = "decision-template" + Path.GetExtension(model.TemplateImage.FileName);
                var templatePhysicalPath = Path.Combine(uploadFolder, templateFileName);

                await using var stream = new FileStream(templatePhysicalPath, FileMode.Create);
                await model.TemplateImage.CopyToAsync(stream);

                templatePath = "/uploads/letters/" + templateFileName;
            }

            if (model.SignatureImage != null && model.SignatureImage.Length > 0)
            {
                var signatureFileName = "decision-signature" + Path.GetExtension(model.SignatureImage.FileName);
                var signaturePhysicalPath = Path.Combine(uploadFolder, signatureFileName);

                await using var stream = new FileStream(signaturePhysicalPath, FileMode.Create);
                await model.SignatureImage.CopyToAsync(stream);

                signaturePath = "/uploads/letters/" + signatureFileName;
            }

            var fields = ParseFields(model.FieldsJson);
            if (!fields.Any())
                fields = GetDefaultFields();

            NormalizeFields(fields, model.PageWidthPx, model.PageHeightPx);

            var settings = new DecisionLetterTemplateSettings
            {
                CurrentTemplateImagePath = templatePath,
                CurrentSignatureImagePath = signaturePath,
                ChairmanName = model.ChairmanName,
                ChairmanTitle = model.ChairmanTitle,
                PageWidthPx = model.PageWidthPx <= 0 ? 794 : model.PageWidthPx,
                PageHeightPx = model.PageHeightPx <= 0 ? 1123 : model.PageHeightPx,
                Fields = fields
            };

            var json = JsonSerializer.Serialize(settings, _jsonOptions);
            System.IO.File.WriteAllText(GetSettingsFilePath(), json);

            TempData["SuccessMessage"] = "Karar mektubu şablonu başarıyla kaydedildi.";
            return RedirectToAction(nameof(Edit));
        }

        private string GetSettingsFilePath()
        {
            var folder = Path.Combine(_environment.ContentRootPath, "App_Data");
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, "decision-letter-template.json");
        }

        private DecisionLetterTemplateSettings? LoadExistingSettings()
        {
            var settingsFile = GetSettingsFilePath();

            if (!System.IO.File.Exists(settingsFile))
                return null;

            var json = System.IO.File.ReadAllText(settingsFile);
            return JsonSerializer.Deserialize<DecisionLetterTemplateSettings>(json, _jsonOptions);
        }

        private List<DecisionLetterFieldSetting> ParseFields(string? fieldsJson)
        {
            if (string.IsNullOrWhiteSpace(fieldsJson))
                return new List<DecisionLetterFieldSetting>();

            try
            {
                var parsed = JsonSerializer.Deserialize<List<DecisionLetterFieldSetting>>(fieldsJson, _jsonOptions);
                return parsed ?? new List<DecisionLetterFieldSetting>();
            }
            catch
            {
                return new List<DecisionLetterFieldSetting>();
            }
        }

        private void NormalizeFields(List<DecisionLetterFieldSetting> fields, int pageWidthPx, int pageHeightPx)
        {
            var width = pageWidthPx <= 0 ? 794 : pageWidthPx;
            var height = pageHeightPx <= 0 ? 1123 : pageHeightPx;

            foreach (var field in fields)
            {
                field.Key ??= string.Empty;
                field.Label ??= field.Key;
                field.Width = Math.Clamp(field.Width <= 0 ? 160 : field.Width, 60, width);
                field.Height = Math.Clamp(field.Height <= 0 ? 28 : field.Height, 20, 200);
                field.X = Math.Clamp(field.X, 0, Math.Max(0, width - field.Width));
                field.Y = Math.Clamp(field.Y, 0, Math.Max(0, height - field.Height));
                field.FontSize = Math.Clamp(field.FontSize <= 0 ? 11 : field.FontSize, 8, 32);
            }
        }

        private List<DecisionLetterFieldSetting> GetDefaultFields()
        {
            return new List<DecisionLetterFieldSetting>
            {
                new() { Key = "ConferenceName", Label = "{ConferenceName}", X = 80, Y = 90, Width = 320, Height = 28, FontSize = 16, Bold = true, Visible = true },
                new() { Key = "DecisionTitle", Label = "{DecisionTitle}", X = 80, Y = 125, Width = 280, Height = 28, FontSize = 15, Bold = true, Visible = true },
                new() { Key = "SubmissionNo", Label = "{SubmissionNo}", X = 80, Y = 170, Width = 210, Height = 24, FontSize = 11, Bold = false, Visible = true },
                new() { Key = "AuthorName", Label = "{AuthorName}", X = 80, Y = 200, Width = 280, Height = 24, FontSize = 11, Bold = false, Visible = true },
                new() { Key = "PaperTitle", Label = "{PaperTitle}", X = 80, Y = 230, Width = 500, Height = 48, FontSize = 11, Bold = true, Visible = true },
                new() { Key = "Date", Label = "{Date}", X = 80, Y = 285, Width = 180, Height = 24, FontSize = 11, Bold = false, Visible = true },
                new() { Key = "DecisionNote", Label = "{DecisionNote}", X = 80, Y = 330, Width = 560, Height = 90, FontSize = 11, Bold = false, Visible = true },
                new() { Key = "Signature", Label = "{Signature}", X = 80, Y = 820, Width = 150, Height = 70, FontSize = 11, Bold = false, Visible = true },
                new() { Key = "ChairmanName", Label = "{ChairmanName}", X = 80, Y = 900, Width = 250, Height = 24, FontSize = 11, Bold = true, Visible = true },
                new() { Key = "ChairmanTitle", Label = "{ChairmanTitle}", X = 80, Y = 930, Width = 320, Height = 24, FontSize = 11, Bold = false, Visible = true }
            };
        }
    }
}
