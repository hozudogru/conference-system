using ConferenceSystem.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class ImportantDatesController : Controller
    {
        private readonly AppDbContext _context;

        public ImportantDatesController(AppDbContext context)
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
                return View(new List<ConferenceSystem.Web.Entities.ImportantDate>());

            var dates = await _context.ImportantDates
                .Where(x => x.ConferenceId == activeConference.Id && x.IsActive)
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.DateValue)
                .ToListAsync();

            return View(dates);
        }
    }
}