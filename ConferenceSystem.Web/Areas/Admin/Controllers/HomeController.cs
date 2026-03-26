using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalSubmissions = await _context.Submissions.CountAsync(),
                TotalReviewers = await _context.Reviewers.CountAsync(),
                TotalAssignments = await _context.ReviewAssignments.CountAsync(),
                TotalReviews = await _context.Reviews.CountAsync(),

                PendingSubmissions = await _context.Submissions.CountAsync(x => x.Status == "Pending"),
                UnderReviewSubmissions = await _context.Submissions.CountAsync(x => x.Status == "Under Review"),
                ReviewedSubmissions = await _context.Submissions.CountAsync(x => x.Status == "Reviewed"),
                AcceptedSubmissions = await _context.Submissions.CountAsync(x => x.Status == "Accepted"),
                RevisionRequiredSubmissions = await _context.Submissions.CountAsync(x => x.Status == "Revision Required"),
                RejectedSubmissions = await _context.Submissions.CountAsync(x => x.Status == "Rejected"),
                RevisedSubmittedSubmissions = await _context.Submissions.CountAsync(x => x.Status == "Revised Submitted"),

                TotalConferences = await _context.Conferences.CountAsync(),
                TotalAnnouncements = await _context.Announcements.CountAsync(),

                LatestSubmissions = await _context.Submissions
                    .Include(x => x.Conference)
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(5)
                    .ToListAsync(),

                LatestDecisions = await _context.EditorialDecisions
                    .Include(x => x.Submission)
                    .OrderByDescending(x => x.DecidedAt)
                    .Take(5)
                    .ToListAsync(),

                LatestAssignments = await _context.ReviewAssignments
                    .Include(x => x.Submission)
                    .Include(x => x.Reviewer)
                    .OrderByDescending(x => x.AssignedAt)
                    .Take(5)
                    .ToListAsync(),
                StatusLabels = new List<string>
            {
                    "Pending",
                    "Under Review",
                    "Reviewed",
                    "Accepted",
                    "Revision Required",
                    "Rejected",
                    "Revised Submitted"
            },

                StatusCounts = new List<int>
            {
                    await _context.Submissions.CountAsync(x => x.Status == "Pending"),
                    await _context.Submissions.CountAsync(x => x.Status == "Under Review"),
                    await _context.Submissions.CountAsync(x => x.Status == "Reviewed"),
                    await _context.Submissions.CountAsync(x => x.Status == "Accepted"),
                    await _context.Submissions.CountAsync(x => x.Status == "Revision Required"),
                    await _context.Submissions.CountAsync(x => x.Status == "Rejected"),
                    await _context.Submissions.CountAsync(x => x.Status == "Revised Submitted")
            }
            };


            return View(model);
        }
    }
}