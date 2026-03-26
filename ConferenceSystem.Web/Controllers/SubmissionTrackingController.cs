using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class SubmissionTrackingController : Controller
    {
        private readonly AppDbContext _context;

        public SubmissionTrackingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SubmissionTrackingSearchViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SubmissionTrackingSearchViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var submission = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.SubmissionNumber == model.SubmissionNumber && x.Email == model.Email);

            if (submission == null)
            {
                ModelState.AddModelError("", "Başvuru bulunamadı. Başvuru numarası ve e-posta bilgisini kontrol ediniz.");
                return View(model);
            }

            var decision = await _context.EditorialDecisions
                .Where(x => x.SubmissionId == submission.Id)
                .OrderByDescending(x => x.DecidedAt)
                .FirstOrDefaultAsync();

            var result = new SubmissionTrackingResultViewModel
            {
                SubmissionId = submission.Id,
                ConferenceName = submission.Conference?.Name ?? "",
                Title = submission.Title,
                AuthorFullName = submission.AuthorFullName,
                Email = submission.Email,
                Institution = submission.Institution,
                Status = submission.Status,
                CreatedAt = submission.CreatedAt,
                DecisionType = decision?.DecisionType ?? "",
                DecisionNote = decision?.DecisionNote ?? "",
                HasDecision = decision != null
            };

            return View("Result", result);
        }
    }
}