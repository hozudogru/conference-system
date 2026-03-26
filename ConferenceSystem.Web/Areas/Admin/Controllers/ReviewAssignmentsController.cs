using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConferenceSystem.Web.Models;
using ConferenceSystem.Web.Services;
using Microsoft.Extensions.Options;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewAssignmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
        private readonly AppSettings _appSettings;
        private readonly AuditService _auditService;
        public ReviewAssignmentsController(
             AppDbContext context,
             EmailService emailService,
             Microsoft.Extensions.Options.IOptions<AppSettings> appSettings,
             AuditService auditService)
        {
            _context = context;
            _emailService = emailService;
            _appSettings = appSettings.Value;
            _auditService = auditService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.ReviewAssignments
                .Include(x => x.Submission)
                .Include(x => x.Reviewer)
                .OrderByDescending(x => x.AssignedAt)
                .ToListAsync();

            return View(items);
        }

        public async Task<IActionResult> Create(long submissionId)
        {
            var submission = await _context.Submissions.FindAsync(submissionId);
            if (submission == null)
                return NotFound();

            ViewBag.SubmissionTitle = submission.Title;
            ViewBag.SubmissionId = submission.Id;

            await LoadReviewers();
            return View(new ReviewAssignment
            {
                SubmissionId = submission.Id,
                Status = "Assigned"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewAssignment model)
        {
            if (!ModelState.IsValid)
            {
                var submission = await _context.Submissions.FindAsync(model.SubmissionId);
                ViewBag.SubmissionTitle = submission?.Title ?? "";
                ViewBag.SubmissionId = model.SubmissionId;
                await LoadReviewers(model.ReviewerId);
                return View(model);
            }

            model.AssignedAt = DateTime.Now;

            _context.ReviewAssignments.Add(model);

            var submissionEntity = await _context.Submissions.FindAsync(model.SubmissionId);
            if (submissionEntity != null)
            {
                submissionEntity.Status = "Under Review";
            }

            await _context.SaveChangesAsync();

            var reviewer = await _context.Reviewers.FindAsync(model.ReviewerId);
            var submissionForMail = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == model.SubmissionId);
                        await _auditService.LogAsync(
                            "Assign",
                            "ReviewAssignment",
                            model.Id,
                            "Admin",
                            $"SubmissionId={model.SubmissionId} hakem {model.ReviewerId} kullanıcısına atandı.");
            if (reviewer != null && submissionForMail != null && !string.IsNullOrWhiteSpace(reviewer.Email))
            {
                var reviewerPanelUrl = $"{_appSettings.BaseUrl}/ReviewerPanel/Login";

                try
                {
                    await _emailService.SendReviewerAssignmentEmailAsync(
                        reviewer.Email,
                        reviewer.FullName,
                        submissionForMail.Conference?.Name ?? "Conference",
                        submissionForMail.Title,
                        reviewerPanelUrl
                    );
                }
                catch
                {
                }
            }

            TempData["Success"] = "Hakem ataması yapıldı ve bilgilendirme maili gönderildi.";
            return RedirectToAction("Detail", "Submissions", new { area = "Admin", id = model.SubmissionId });


        }

        private async Task LoadReviewers(object? selectedValue = null)
        {
            var reviewers = await _context.Reviewers
                .Where(x => x.IsActive)
                .OrderBy(x => x.FullName)
                .ToListAsync();

            ViewBag.Reviewers = new SelectList(reviewers, "Id", "FullName", selectedValue);
        }
    }
}