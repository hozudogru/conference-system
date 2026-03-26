using ConferenceSystem.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Services
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ReminderBackgroundService> _logger;

        public ReminderBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<ReminderBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ReminderBackgroundService başladı.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                    await SendReviewerRemindersAsync(context, emailService, stoppingToken);
                    await SendRevisionRemindersAsync(context, emailService, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("ReminderBackgroundService iptal edildi.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ReminderBackgroundService içinde hata oluştu.");
                }

                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("ReminderBackgroundService delay aşamasında durduruldu.");
                    break;
                }
            }

            _logger.LogInformation("ReminderBackgroundService durdu.");
        }

        private async Task SendReviewerRemindersAsync(
            AppDbContext context,
            EmailService emailService,
            CancellationToken stoppingToken)
        {
            var pendingAssignments = await context.ReviewAssignments
                .Include(x => x.Reviewer)
                .Include(x => x.Submission!)
                    .ThenInclude(x => x.Conference)
                .Where(x =>
                    x.Status != "Completed" &&
                    x.Reviewer != null &&
                    x.Submission != null &&
                    x.Submission.Conference != null &&
                    x.AssignedAt <= DateTime.Now.AddDays(-3))
                .ToListAsync(stoppingToken);

            foreach (var item in pendingAssignments)
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                try
                {
                    if (string.IsNullOrWhiteSpace(item.Reviewer?.Email))
                        continue;

                    var reviewerName = string.IsNullOrWhiteSpace(item.Reviewer?.FullName)
                        ? "Reviewer"
                        : item.Reviewer.FullName;

                    var conferenceName = item.Submission?.Conference?.Name ?? "Conference";
                    var submissionTitle = item.Submission?.Title ?? "Submission";
                    var reviewerPanelUrl = "/Reviewer";

                    await emailService.SendReviewerReminderEmailAsync(
                        item.Reviewer!.Email!,
                        reviewerName,
                        conferenceName,
                        submissionTitle,
                        reviewerPanelUrl
                    );

                    _logger.LogInformation(
                        "Hakem hatırlatma maili gönderildi. Reviewer={ReviewerEmail}, SubmissionId={SubmissionId}",
                        item.Reviewer.Email,
                        item.SubmissionId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Hakem hatırlatma maili gönderilemedi. AssignmentId={AssignmentId}",
                        item.Id);
                }
            }
        }

        private async Task SendRevisionRemindersAsync(
            AppDbContext context,
            EmailService emailService,
            CancellationToken stoppingToken)
        {
            var revisionNeededSubmissions = await context.Submissions
                .Include(x => x.Conference)
                .Where(x =>
                    x.Status == "Revision Required" &&
                    x.Email != null &&
                    x.Email != "" &&
                    x.CreatedAt <= DateTime.Now.AddDays(-5))
                .ToListAsync(stoppingToken);

            foreach (var item in revisionNeededSubmissions)
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                try
                {
                    var toEmail = item.Email!;
                    var toName = string.IsNullOrWhiteSpace(item.AuthorFullName)
                        ? "Author"
                        : item.AuthorFullName;

                    var conferenceName = item.Conference?.Name ?? "Conference";
                    var submissionTitle = item.Title ?? "Submission";
                    var submissionNumber = item.SubmissionNumber ?? "-";
                    var authorPanelUrl = "/Author";

                    await emailService.SendRevisionReminderEmailAsync(
                        toEmail,
                        toName,
                        conferenceName,
                        submissionTitle,
                        submissionNumber,
                        authorPanelUrl
                    );

                    _logger.LogInformation(
                        "Revizyon hatırlatma maili gönderildi. Email={Email}, SubmissionId={SubmissionId}",
                        toEmail,
                        item.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Revizyon hatırlatma maili gönderilemedi. SubmissionId={SubmissionId}",
                        item.Id);
                }
            }
        }
    }
}