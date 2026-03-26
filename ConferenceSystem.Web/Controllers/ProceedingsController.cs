using ConferenceSystem.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class ProceedingsController : Controller
    {
        private readonly AppDbContext _context;

        public ProceedingsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Proceedings
                .Include(x => x.Submission)
                    .ThenInclude(x => x!.Conference)
                .OrderByDescending(x => x.PublishedAt)
                .ToListAsync();

            return View(list);
        }
    }
}