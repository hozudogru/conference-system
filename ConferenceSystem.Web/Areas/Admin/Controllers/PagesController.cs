using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly AppDbContext _context;

        public PagesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pages = await _context.Pages
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Title)
                .ToListAsync();

            return View(pages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentPage model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.CreatedAt = DateTime.Now;

            _context.Pages.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
                return NotFound();

            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ContentPage model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
                return NotFound();

            page.Title = model.Title;
            page.Slug = model.Slug;
            page.Content = model.Content;
            page.MetaTitle = model.MetaTitle;
            page.MetaDescription = model.MetaDescription;
            page.IsPublished = model.IsPublished;
            page.SortOrder = model.SortOrder;
            page.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
                return NotFound();

            return View(page);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
                return NotFound();

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}