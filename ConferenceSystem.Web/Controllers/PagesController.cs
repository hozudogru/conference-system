using ConferenceSystem.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Controllers
{
    [Route("sayfa")]
    public class PagesController : Controller
    {
        private readonly AppDbContext _context;

        public PagesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var page = await _context.Pages
                .FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished);

            if (page == null)
                return NotFound();

            return View(page);
        }
    }
}