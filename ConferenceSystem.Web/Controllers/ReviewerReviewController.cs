using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConferenceSystem.Web.Services;

namespace ConferenceSystem.Web.Controllers
{
    public class ReviewerReviewController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AuditService _auditService;
        public ReviewerReviewController(AppDbContext context, AuditService auditService)
        {
            _context = context;
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(long reviewAssignmentId)
        {
            var assignment = await _context.ReviewAssignments
                .Include(x => x.Submission)
                .Include(x => x.Reviewer)
                .FirstOrDefaultAsync(x => x.Id == reviewAssignmentId);

            if (assignment == null)
                return NotFound();

            ViewBag.AssignmentInfo = $"{assignment.Submission?.Title} / {assignment.Reviewer?.FullName}";
            ViewBag.AssignmentId = assignment.Id;

            LoadRecommendations("Revise");

            return View(new Review
            {
                ReviewAssignmentId = assignment.Id,
                Recommendation = "Revise"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review model)
        {
            var assignment = await _context.ReviewAssignments
                .Include(x => x.Submission)
                .Include(x => x.Reviewer)
                .FirstOrDefaultAsync(x => x.Id == model.ReviewAssignmentId);

            if (assignment == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.AssignmentInfo = $"{assignment.Submission?.Title} / {assignment.Reviewer?.FullName}";
                ViewBag.AssignmentId = assignment.Id;
                LoadRecommendations(model.Recommendation);
                return View(model);
            }

            model.SubmittedAt = DateTime.Now;
            _context.Reviews.Add(model);

            assignment.Status = "Completed";

            if (assignment.Submission != null)
            {
                assignment.Submission.Status = "Reviewed";
            }

            await _context.SaveChangesAsync();
            await _auditService.LogAsync(
                            "Create",
                            "Review",
                            model.Id,
                            assignment.Reviewer?.Email ?? "Reviewer",
              $"ReviewAssignmentId={model.ReviewAssignmentId} için değerlendirme girildi. Recommendation={model.Recommendation}");
            return RedirectToAction("Assignments", "ReviewerPanel", new { reviewerId = assignment.ReviewerId });
           
        }

        private void LoadRecommendations(string? selectedValue = null)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = "Accept", Text = "Accept" },
                new SelectListItem { Value = "Revise", Text = "Revise" },
                new SelectListItem { Value = "Reject", Text = "Reject" }
            };

            ViewBag.Recommendations = new SelectList(items, "Value", "Text", selectedValue);
        }
    }
}