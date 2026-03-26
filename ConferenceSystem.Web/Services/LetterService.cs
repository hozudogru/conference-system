using ConferenceSystem.Web.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ConferenceSystem.Web.Services
{
    public class LetterService
    {
        public byte[] GenerateDecisionLetter(Submission submission, string decisionType, string decisionNote)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var title = decisionType switch
            {
                "Accepted" => "ACCEPTANCE LETTER",
                "Rejected" => "REJECTION LETTER",
                "Revision Required" => "REVISION LETTER",
                _ => "DECISION LETTER"
            };

            var bodyText = decisionType switch
            {
                "Accepted" => $"""
Dear {submission.AuthorFullName},

We are pleased to inform you that your submission titled:

"{submission.Title}"

has been ACCEPTED for presentation/publication.

Conference: {submission.Conference?.Name}

We congratulate you and thank you for your contribution.
""",

                "Rejected" => $"""
Dear {submission.AuthorFullName},

We regret to inform you that your submission titled:

"{submission.Title}"

has been REJECTED after editorial evaluation.

Conference: {submission.Conference?.Name}

Thank you for your interest and contribution.
""",

                "Revision Required" => $"""
Dear {submission.AuthorFullName},

Your submission titled:

"{submission.Title}"

has been evaluated and requires REVISION before a final decision can be made.

Conference: {submission.Conference?.Name}

Please review the comments and submit your revised version.
""",

                _ => $"""
Dear {submission.AuthorFullName},

A decision has been made regarding your submission titled:

"{submission.Title}"

Conference: {submission.Conference?.Name}
"""
            };

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);

                    page.Header().Column(col =>
                    {
                        col.Item()
                            .AlignCenter()
                            .Text(submission.Conference?.Name ?? "Conference")
                            .FontSize(18)
                            .Bold();

                        col.Item()
                            .AlignCenter()
                            .Text(title)
                            .FontSize(16)
                            .Bold();
                    });

                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        col.Item()
                            .Text(bodyText)
                            .FontSize(12)
                            .LineHeight(1.5f);

                        if (!string.IsNullOrWhiteSpace(decisionNote))
                        {
                            col.Item()
                                .PaddingTop(15)
                                .Text("Editorial Note:")
                                .Bold()
                                .FontSize(12);

                            col.Item()
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(10)
                                .Text(decisionNote)
                                .FontSize(11);
                        }

                        col.Item()
                            .PaddingTop(25)
                            .Text("Best Regards,")
                            .FontSize(12);

                        col.Item()
                            .Text("Conference Committee")
                            .FontSize(12)
                            .Bold();
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated on {DateTime.Now:dd.MM.yyyy HH:mm}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken1);
                });
            });

            return document.GeneratePdf();
        }
    }
}