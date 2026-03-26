using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class PublicLettersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LetterService _letterService;

        public PublicLettersController(AppDbContext context, LetterService letterService)
        {
            _context = context;
            _letterService = letterService;
        }

        public async Task<IActionResult> Download(long submissionId)
        {
            var submission = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == submissionId);

            if (submission == null)
                return NotFound();

            var decision = await _context.EditorialDecisions
                .Where(x => x.SubmissionId == submissionId)
                .OrderByDescending(x => x.DecidedAt)
                .FirstOrDefaultAsync();

            if (decision == null)
                return NotFound("Henüz karar bulunamadı.");

            var pdf = _letterService.GenerateDecisionLetter(
                submission,
                decision.DecisionType,
                decision.DecisionNote
            );

            var fileName = $"{decision.DecisionType.Replace(" ", "_").ToLower()}_letter_{submissionId}.pdf";

            return File(pdf, "application/pdf", fileName);
        }
    }
}