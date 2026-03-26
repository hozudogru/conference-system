using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class ReviewerPanelController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewerPanelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => RedirectToAction("Login", "Access");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ReviewerLoginViewModel model) => RedirectToAction("Login", "Access", new { email = model.Email });

        [HttpGet]
        public IActionResult RequestMagicLink() => RedirectToAction("Login", "Access");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestMagicLink(ReviewerLoginViewModel model) => RedirectToAction("Login", "Access", new { email = model.Email });

        [HttpGet]
        public IActionResult MagicLogin(string token) => RedirectToAction("MagicLogin", "Access", new { token });

        [HttpGet]
        public async Task<IActionResult> Assignments(long reviewerId)
        {
            if (!CanAccessReviewer(reviewerId))
                return RedirectToAction("Login", "Access");

            var reviewer = await _context.Reviewers.FindAsync(reviewerId);
            if (reviewer == null)
                return NotFound();

            ViewBag.Reviewer = reviewer;

            var assignments = await _context.ReviewAssignments
                .Include(x => x.Submission)
                    .ThenInclude(x => x!.Conference)
                .Where(x => x.ReviewerId == reviewerId)
                .OrderByDescending(x => x.AssignedAt)
                .ToListAsync();

            return View(assignments);
        }

        private bool CanAccessReviewer(long reviewerId)
        {
            var role = HttpContext.Session.GetString("PortalRole");
            var sessionReviewerId = HttpContext.Session.GetString("ReviewerId");
            return role == "Reviewer" && sessionReviewerId == reviewerId.ToString();
        }
    }
}
