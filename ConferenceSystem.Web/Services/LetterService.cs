using ConferenceSystem.Web.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.Json;
using ConferenceSystem.Web.ViewModels;

namespace ConferenceSystem.Web.Services
{
    public class LetterService
    {
        private readonly SiteSettingsService _siteSettingsService;
        private readonly IWebHostEnvironment _environment;

        public LetterService(SiteSettingsService siteSettingsService, IWebHostEnvironment environment)
        {
            _siteSettingsService = siteSettingsService;
            _environment = environment;
        }

        private DecisionLetterTemplateSettings? LoadDecisionLetterSettings()
        {
            var settingsPath = Path.Combine(_environment.ContentRootPath, "App_Data", "decision-letter-template.json");

            if (!File.Exists(settingsPath))
                return null;

            var json = File.ReadAllText(settingsPath);
            return JsonSerializer.Deserialize<DecisionLetterTemplateSettings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public byte[] GenerateDecisionLetter(Submission submission, string decisionType, string decisionNote)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            const float pdfPageWidth = 595f;
            const float pdfPageHeight = 842f;

            var settings = LoadDecisionLetterSettings();
            var pageWidthPx = settings?.PageWidthPx > 0 ? settings.PageWidthPx : 794;
            var pageHeightPx = settings?.PageHeightPx > 0 ? settings.PageHeightPx : 1123;

            var scaleX = pdfPageWidth / pageWidthPx;
            var scaleY = pdfPageHeight / pageHeightPx;

            var templateImagePath = MapWebPath(
                settings?.CurrentTemplateImagePath ?? GetSetting("Letter.TemplateImagePath")
            );

            var signatureImagePath = MapWebPath(
                settings?.CurrentSignatureImagePath ?? GetSetting("Letter.SignatureImagePath")
            );

            var chairmanName = !string.IsNullOrWhiteSpace(settings?.ChairmanName)
                ? settings!.ChairmanName!
                : GetSetting("Letter.ChairmanName", "Conference Committee");

            var chairmanTitle = !string.IsNullOrWhiteSpace(settings?.ChairmanTitle)
                ? settings!.ChairmanTitle!
                : GetSetting("Letter.ChairmanTitle", "Conference Organizing Committee");

            var letterTitle = decisionType switch
            {
                "Accepted" => "ACCEPTANCE LETTER",
                "Rejected" => "REJECTION LETTER",
                "Revision Required" => "REVISION LETTER",
                _ => "DECISION LETTER"
            };

            var conferenceName = submission.Conference?.Name ?? "Conference";
            var submissionNumber = string.IsNullOrWhiteSpace(submission.SubmissionNumber)
                ? submission.Id.ToString()
                : submission.SubmissionNumber;

            var bodyText = decisionType switch
            {
                "Accepted" => "Your submission has been accepted for the conference/program.",
                "Rejected" => "Your submission has not been accepted after the evaluation process.",
                "Revision Required" => "Your submission requires revision before a final decision can be made.",
                _ => "A decision has been made regarding your submission."
            };

            var decisionNoteText = string.IsNullOrWhiteSpace(decisionNote)
                ? bodyText
                : $"Editorial Note: {decisionNote}";

            var fields = MergeWithDefaults(settings?.Fields);

            var values = new Dictionary<string, string>
            {
                ["ConferenceName"] = conferenceName,
                ["DecisionTitle"] = letterTitle,
                ["SubmissionNo"] = $"Submission No: {submissionNumber}",
                ["AuthorName"] = $"Author: {submission.AuthorFullName}",
                ["PaperTitle"] = $"Paper Title: {submission.Title}",
                ["Date"] = $"Date: {DateTime.Now:dd.MM.yyyy}",
                ["Decision"] = decisionType,
                ["DecisionNote"] = decisionNoteText,
                ["ChairmanName"] = chairmanName,
                ["ChairmanTitle"] = chairmanTitle
            };

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(0);

                    page.Content().Layers(layers =>
                    {
                        layers.PrimaryLayer().Text(string.Empty);

                        if (!string.IsNullOrWhiteSpace(templateImagePath) && File.Exists(templateImagePath))
                        {
                            layers.Layer().Image(templateImagePath).FitArea();
                        }

                        foreach (var field in fields.Where(x => x.Visible))
                        {
                            var x = field.X * scaleX;
                            var y = field.Y * scaleY;
                            var width = Math.Max(40, field.Width * scaleX);
                            var height = Math.Max(18, field.Height * scaleY);
                            var fontSize = Math.Max(8, field.FontSize * scaleX);

                            if (field.Key == "Signature")
                            {
                                if (!string.IsNullOrWhiteSpace(signatureImagePath) && File.Exists(signatureImagePath))
                                {
                                    layers.Layer()
                                        .PaddingLeft(x)
                                        .PaddingTop(y)
                                        .Width(width)
                                        .Height(height)
                                        .Image(signatureImagePath)
                                        .FitWidth();
                                }

                                continue;
                            }

                            if (!values.TryGetValue(field.Key, out var textValue))
                                textValue = string.IsNullOrWhiteSpace(field.Label) ? field.Key : field.Label;

                            layers.Layer()
                                .PaddingLeft(x)
                                .PaddingTop(y)
                                .Width(width)
                                .Height(height)
                                .Text(text =>
                                {
                                    var span = text.Span(textValue).FontSize(fontSize);
                                    if (field.Bold)
                                        span.Bold();
                                });
                        }
                    });
                });
            });

            return document.GeneratePdf();
        }

        private List<DecisionLetterFieldSetting> MergeWithDefaults(List<DecisionLetterFieldSetting>? savedFields)
        {
            var defaults = GetFallbackFields();

            if (savedFields == null || !savedFields.Any())
                return defaults;

            var result = new List<DecisionLetterFieldSetting>();

            foreach (var def in defaults)
            {
                var saved = savedFields.FirstOrDefault(x => x.Key == def.Key);

                if (saved == null)
                {
                    result.Add(def);
                    continue;
                }

                result.Add(new DecisionLetterFieldSetting
                {
                    Key = saved.Key,
                    Label = string.IsNullOrWhiteSpace(saved.Label) ? def.Label : saved.Label,
                    X = saved.X,
                    Y = saved.Y,
                    Width = saved.Width <= 0 ? def.Width : saved.Width,
                    Height = saved.Height <= 0 ? def.Height : saved.Height,
                    FontSize = saved.FontSize <= 0 ? def.FontSize : saved.FontSize,
                    Bold = saved.Bold,
                    Visible = saved.Visible
                });
            }

            return result;
        }

        private List<DecisionLetterFieldSetting> GetFallbackFields()
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

        private string GetSetting(string key, string defaultValue = "")
        {
            return _siteSettingsService.GetValueAsync(key, defaultValue).GetAwaiter().GetResult();
        }

        private string MapWebPath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            path = path.Replace('\\', '/');

            if (Path.IsPathRooted(path) && File.Exists(path))
                return path;

            if (path.StartsWith("/"))
                path = path.TrimStart('/');

            var fullPath = Path.Combine(_environment.WebRootPath, path.Replace('/', Path.DirectorySeparatorChar));

            return File.Exists(fullPath) ? fullPath : string.Empty;
        }
    }
}
