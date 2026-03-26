using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class ProgramController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ProgramBookletService _programBookletService;

        public ProgramController(AppDbContext context, ProgramBookletService programBookletService)
        {
            _context = context;
            _programBookletService = programBookletService;
        }

        public async Task<IActionResult> Index(long conferenceId)
        {
            var conference = await _context.Conferences.FindAsync(conferenceId);
            if (conference == null)
                return NotFound();

            var sessions = await _context.Sessions
                .Include(x => x.Items.OrderBy(i => i.Order))
                    .ThenInclude(x => x.Submission)
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.StartTime)
                .ThenBy(x => x.Room)
                .ToListAsync();

            ViewBag.Conference = conference;
            ViewBag.ConferenceId = conferenceId;
            return View(sessions);
        }

        public async Task<IActionResult> Booklet(long conferenceId)
        {
            var conference = await _context.Conferences.FindAsync(conferenceId);
            if (conference == null)
                return NotFound();

            var sessions = await _context.Sessions
                .Include(x => x.Items.OrderBy(i => i.Order))
                    .ThenInclude(x => x.Submission)
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.StartTime)
                .ThenBy(x => x.Room)
                .ToListAsync();

            var pdf = _programBookletService.GenerateProgramBooklet(conference, sessions);

            return File(pdf, "application/pdf", $"program_booklet_{conferenceId}.pdf");
        }
    }
}