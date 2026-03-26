using ConferenceSystem.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly AppDbContext _context;

        public AnnouncementsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _context.Announcements
                .Where(x => x.IsPublished)
                .OrderByDescending(x => x.PublishDate)
                .ToListAsync();

            return View(announcements);
        }

        public async Task<IActionResult> Detail(long id)
        {
            var announcement = await _context.Announcements
                .FirstOrDefaultAsync(x => x.Id == id && x.IsPublished);

            if (announcement == null)
                return NotFound();

            return View(announcement);
        }
    }
}