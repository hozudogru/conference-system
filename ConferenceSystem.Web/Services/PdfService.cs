using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ConferenceSystem.Web.Services
{
    public class PdfService
    {
        public byte[] GenerateDecisionLetter(
            string title,
            string author,
            string paperId,
            string decision,
            string chairmanName,
            string chairmanTitle,
            string templateImagePath,
            string signatureImagePath)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(0);

                    page.Content().Layers(layers =>
                    {
                        // 🖼️ ARKA PLAN ŞABLON
                        if (!string.IsNullOrEmpty(templateImagePath) && File.Exists(templateImagePath))
                        {
                            layers.Layer().Image(templateImagePath).FitArea();
                        }

                        // 📝 YAZILAR
                        layers.PrimaryLayer().Padding(50).Column(col =>
                        {
                            col.Spacing(10);

                            col.Item().Text($"Bildiri No: {paperId}").FontSize(12);
                            col.Item().Text($"Başlık: {title}").FontSize(14).Bold();
                            col.Item().Text($"Yazar: {author}").FontSize(12);

                            col.Item().Text($"Karar: {decision}")
                                .FontSize(14)
                                .Bold()
                                .FontColor(Colors.Green.Darken2);

                            col.Item().Text($"Tarih: {DateTime.Now:dd.MM.yyyy}")
                                .FontSize(10);

                            col.Item().PaddingTop(50);

                            // ✍️ İMZA
                            if (!string.IsNullOrEmpty(signatureImagePath) && File.Exists(signatureImagePath))
                            {
                                col.Item().Width(150).Image(signatureImagePath);
                            }

                            col.Item().Text(chairmanName).Bold();
                            col.Item().Text(chairmanTitle);
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}