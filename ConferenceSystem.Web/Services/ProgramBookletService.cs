using ConferenceSystem.Web.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ConferenceSystem.Web.Services
{
    public class ProgramBookletService
    {
        public byte[] GenerateProgramBooklet(Conference conference, List<Session> sessions)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingTop(30);

                        if (!string.IsNullOrWhiteSpace(conference.LogoPath))
                        {
                            try
                            {
                                var logoPath = conference.LogoPath
                                    .TrimStart('/')
                                    .Replace("/", Path.DirectorySeparatorChar.ToString());

                                var fullLogoPath = Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    "wwwroot",
                                    logoPath);

                                if (File.Exists(fullLogoPath))
                                {
                                    col.Item().AlignCenter().Height(90).Element(c =>
                                    {
                                        c.Image(fullLogoPath).FitHeight();
                                    });
                                }
                            }
                            catch
                            {
                            }
                        }

                        col.Item().PaddingTop(20).AlignCenter().Text(conference.Name)
                            .FontSize(24)
                            .Bold();

                        col.Item().PaddingTop(15).AlignCenter().Text("PROGRAM BOOKLET")
                            .FontSize(18)
                            .Bold()
                            .FontColor(Colors.Blue.Darken2);

                        col.Item().PaddingTop(25).AlignCenter().Text(
                                $"{conference.StartDate:dd.MM.yyyy} - {conference.EndDate:dd.MM.yyyy}")
                            .FontSize(13);

                        if (!string.IsNullOrWhiteSpace(conference.PresidentName))
                        {
                            col.Item().PaddingTop(15).AlignCenter().Text($"Conference President: {conference.PresidentName}")
                                .FontSize(12)
                                .SemiBold();
                        }

                        if (!string.IsNullOrWhiteSpace(conference.OrganizingCommitteeInfo))
                        {
                            col.Item().PaddingTop(8).AlignCenter().Text($"Organizing Committee: {conference.OrganizingCommitteeInfo}")
                                .FontSize(11)
                                .FontColor(Colors.Grey.Darken1);
                        }

                        col.Item().PaddingTop(120).AlignCenter().Text("Prepared by Conference Management System")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken1);
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Darken1));
                        text.Span("Generated on ");
                        text.Span($"{DateTime.Now:dd.MM.yyyy HH:mm}");
                    });
                });

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(35);

                    page.Header().Column(col =>
                    {
                        col.Item().AlignCenter().Text(conference.Name)
                            .FontSize(18)
                            .Bold();

                        col.Item().AlignCenter().Text("Conference Program Schedule")
                            .FontSize(12)
                            .FontColor(Colors.Grey.Darken2);

                        col.Item().PaddingTop(8)
                            .BorderBottom(1)
                            .BorderColor(Colors.Grey.Lighten2)
                            .PaddingBottom(1);
                    });

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        if (sessions == null || !sessions.Any())
                        {
                            col.Item().Text("No schedule available.")
                                .FontSize(12);
                            return;
                        }

                        foreach (var session in sessions.OrderBy(x => x.StartTime).ThenBy(x => x.Room))
                        {
                            col.Item()
                                .PaddingTop(10)
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Background(Colors.Grey.Lighten4)
                                .Padding(10)
                                .Column(sessionCol =>
                                {
                                    sessionCol.Item().Text(
                                            $"{session.Title} | {session.StartTime:dd.MM.yyyy HH:mm} - {session.EndTime:HH:mm} | {session.Room}")
                                        .FontSize(13)
                                        .Bold()
                                        .FontColor(Colors.Blue.Darken2);

                                    if (!string.IsNullOrWhiteSpace(session.ChairName))
                                    {
                                        sessionCol.Item().PaddingTop(3).Text($"Chair: {session.ChairName}")
                                            .FontSize(10)
                                            .SemiBold();
                                    }

                                    sessionCol.Item().PaddingTop(6)
                                        .BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten2)
                                        .PaddingBottom(1);

                                    sessionCol.Item().PaddingTop(8).Column(itemCol =>
                                    {
                                        foreach (var item in session.Items.OrderBy(x => x.Order))
                                        {
                                            itemCol.Item()
                                                .PaddingBottom(8)
                                                .BorderBottom(1)
                                                .BorderColor(Colors.Grey.Lighten3)
                                                .PaddingBottom(6)
                                                .Column(paperCol =>
                                                {
                                                    paperCol.Item().Text(
                                                            $"{item.Order}. {item.Submission?.Title}")
                                                        .FontSize(12)
                                                        .SemiBold();

                                                    paperCol.Item().PaddingTop(2).Text(item.Submission?.AuthorFullName ?? "")
                                                        .FontSize(10)
                                                        .FontColor(Colors.Grey.Darken1);

                                                    paperCol.Item().PaddingTop(1).Text(
                                                            $"Duration: {item.PresentationMinutes} min" +
                                                            (!string.IsNullOrWhiteSpace(item.Submission?.SubmissionNumber)
                                                                ? $" | Submission No: {item.Submission.SubmissionNumber}"
                                                                : ""))
                                                        .FontSize(9)
                                                        .FontColor(Colors.Grey.Darken1);
                                                });
                                        }
                                    });
                                });
                        }
                    });

                    page.Footer().Row(row =>
                    {
                        row.RelativeItem().Text($"Generated on {DateTime.Now:dd.MM.yyyy HH:mm}")
                            .FontSize(9)
                            .FontColor(Colors.Grey.Darken1);

                        row.ConstantItem(60).AlignRight().Text(text =>
                        {
                            text.DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Darken1));
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}