using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace ConferenceSystem.Web.Services
{
    public class EmailService
    {
        private readonly AppDbContext _context;

        public EmailService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<SmtpSetting> GetSmtpSettingsAsync()
        {
            var smtp = await _context.SmtpSettings
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (smtp == null)
                throw new Exception("SMTP ayarı bulunamadı. Admin > SMTP Ayarları ekranından tanımlayın.");

            return smtp;
        }

        public async Task<string> SendDecisionEmailAsync(
            string toEmail,
            string toName,
            string subject,
            string bodyText,
            byte[]? attachmentBytes = null,
            string? attachmentFileName = null)
        {
            var smtp = await GetSmtpSettingsAsync();

            var logLines = new List<string>();

            void Log(string message)
            {
                var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                logLines.Add(line);
                Console.WriteLine(line);
            }

            Log("=== EMAIL GÖNDERİMİ BAŞLADI ===");
            Log($"HOST            : {smtp.Host}");
            Log($"PORT            : {smtp.Port}");
            Log($"USERNAME        : {smtp.UserName}");
            Log($"FROM EMAIL      : {smtp.FromEmail}");
            Log($"FROM NAME       : {smtp.FromName}");
            Log($"TO EMAIL        : {toEmail}");
            Log($"TO NAME         : {toName}");
            Log($"SUBJECT         : {subject}");
            Log($"USE SSL         : {smtp.UseSsl}");
            Log($"SECURITY MODE   : {smtp.SecurityMode}");
            Log($"REQUIRE AUTH    : {smtp.RequireAuthentication}");
            Log($"TIMEOUT (ms)    : {smtp.TimeoutMilliseconds}");

            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Alıcı e-posta boş olamaz.", nameof(toEmail));

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Mail konusu boş olamaz.", nameof(subject));

            if (string.IsNullOrWhiteSpace(bodyText))
                throw new ArgumentException("Mail içeriği boş olamaz.", nameof(bodyText));

            if (string.IsNullOrWhiteSpace(smtp.Host))
                throw new Exception("SMTP Host boş.");

            if (string.IsNullOrWhiteSpace(smtp.FromEmail))
                throw new Exception("SMTP FromEmail boş.");

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                string.IsNullOrWhiteSpace(smtp.FromName) ? smtp.FromEmail : smtp.FromName,
                smtp.FromEmail));

            message.To.Add(new MailboxAddress(
                string.IsNullOrWhiteSpace(toName) ? toEmail : toName,
                toEmail));

            message.Subject = subject;

            var builder = new BodyBuilder
            {
                TextBody = bodyText
            };

            if (attachmentBytes != null && !string.IsNullOrWhiteSpace(attachmentFileName))
            {
                builder.Attachments.Add(
                    attachmentFileName,
                    attachmentBytes,
                    new ContentType("application", "pdf"));

                Log($"ATTACHMENT      : {attachmentFileName}");
            }

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            client.Timeout = smtp.TimeoutMilliseconds;
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            try
            {
                var mode = (smtp.SecurityMode ?? "Auto").Trim().ToLowerInvariant();

                var options = mode switch
                {
                    "sslonconnect" => SecureSocketOptions.SslOnConnect,
                    "starttls" => SecureSocketOptions.StartTls,
                    "none" => SecureSocketOptions.None,
                    _ => SecureSocketOptions.Auto
                };

                Log($"CONNECTING      : {smtp.Host}:{smtp.Port}");
                await client.ConnectAsync(smtp.Host, smtp.Port, options);
                Log("CONNECTED       : OK");

                if (smtp.RequireAuthentication && !string.IsNullOrWhiteSpace(smtp.UserName))
                {
                    Log("AUTH START      : giriş yapılıyor");
                    await client.AuthenticateAsync(smtp.UserName, smtp.Password);
                    Log("AUTH OK         : başarılı");
                }

                Log("SENDING         : mail gönderiliyor");
                var response = await client.SendAsync(message);
                Log($"SMTP RESPONSE   : {response}");

                await client.DisconnectAsync(true);
                Log("DISCONNECTED    : bağlantı kapandı");

                return string.Join(Environment.NewLine, logLines);
            }
            catch (Exception ex)
            {
                Log("=== HATA ===");
                Log(ex.Message);
                Log(ex.ToString());

                if (client.IsConnected)
                {
                    try { await client.DisconnectAsync(true); } catch { }
                }

                throw new Exception(string.Join(Environment.NewLine, logLines), ex);
            }
        }

        public async Task<string> SendSimpleEmailAsync(
            string toEmail,
            string toName,
            string subject,
            string bodyText)
        {
            return await SendDecisionEmailAsync(toEmail, toName, subject, bodyText);
        }

        public async Task<string> SendSubmissionConfirmationEmailAsync(
            string toEmail,
            string toName,
            string conferenceName,
            string submissionTitle,
            string submissionNumber)
        {
            var subject = $"Submission Received - {conferenceName}";

            var body = $"""
Dear {toName},

Your submission has been successfully received.

Conference: {conferenceName}
Submission Number: {submissionNumber}
Title: {submissionTitle}

Please keep this submission number for future tracking.

Best regards,
Conference Committee
""";

            return await SendDecisionEmailAsync(toEmail, toName, subject, body);
        }

        public async Task<string> SendNewSubmissionAlertToEditorAsync(
            string toEmail,
            string toName,
            string conferenceName,
            string submissionTitle,
            string submissionNumber,
            string authorName,
            string authorEmail)
        {
            var subject = $"New Submission Received - {conferenceName}";

            var body = $"""
Hello {toName},

A new submission has been received.

Conference: {conferenceName}
Submission Number: {submissionNumber}
Title: {submissionTitle}
Author: {authorName}
Author Email: {authorEmail}

Please review it in the admin panel.

Best regards,
Conference System
""";

            return await SendDecisionEmailAsync(toEmail, toName, subject, body);
        }

        public async Task<string> SendReviewerAssignmentEmailAsync(
            string toEmail,
            string toName,
            string conferenceName,
            string submissionTitle,
            string reviewerPanelUrl)
        {
            var subject = $"New Review Assignment - {conferenceName}";

            var body = $"""
Dear {toName},

A new submission has been assigned to you for review.

Conference: {conferenceName}
Submission Title: {submissionTitle}

You can access your reviewer panel using the link below:
{reviewerPanelUrl}

Please use your e-mail address to access the system.

Best regards,
Conference Committee
""";

            return await SendDecisionEmailAsync(toEmail, toName, subject, body);
        }

        public async Task<string> SendReviewerReminderEmailAsync(
            string toEmail,
            string toName,
            string conferenceName,
            string submissionTitle,
            string reviewerPanelUrl)
        {
            var subject = $"Reminder: Review Pending - {conferenceName}";

            var body = $"""
Dear {toName},

This is a reminder that a submission assigned to you is still awaiting review.

Conference: {conferenceName}
Submission Title: {submissionTitle}

Please access your reviewer panel below:
{reviewerPanelUrl}

Best regards,
Conference Committee
""";

            return await SendDecisionEmailAsync(toEmail, toName, subject, body);
        }

        public async Task<string> SendRevisionReminderEmailAsync(
            string toEmail,
            string toName,
            string conferenceName,
            string submissionTitle,
            string submissionNumber,
            string authorPanelUrl)
        {
            var subject = $"Reminder: Revision Pending - {conferenceName}";

            var body = $"""
Dear {toName},

This is a reminder that your submission still requires revision.

Conference: {conferenceName}
Submission Number: {submissionNumber}
Submission Title: {submissionTitle}

You can access your author panel here:
{authorPanelUrl}

Best regards,
Conference Committee
""";

            return await SendDecisionEmailAsync(toEmail, toName, subject, body);
        }
    }
}