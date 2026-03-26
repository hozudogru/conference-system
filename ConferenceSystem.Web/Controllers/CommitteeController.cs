using ConferenceSystem.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class CommitteeController : Controller
    {
        private readonly AppDbContext _context;

        public CommitteeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var activeConference = await _context.Conferences
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            if (activeConference == null)
                return View(new List<ConferenceSystem.Web.Entities.CommitteeMember>());

            var members = await _context.CommitteeMembers
                .Where(x => x.ConferenceId == activeConference.Id && x.IsActive)
                .OrderBy(x => x.CommitteeType)
                .ThenBy(x => x.SortOrder)
                .ThenBy(x => x.FullName)
                .ToListAsync();

            return View(members);
        }
    }
}