using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewersController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Reviewers
                .OrderBy(x => x.FullName)
                .ToListAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            return View(new Reviewer { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reviewer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Reviewers.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var item = await _context.Reviewers.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Reviewer model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var item = await _context.Reviewers.FindAsync(id);
            if (item == null)
                return NotFound();

            item.FullName = model.FullName;
            item.Email = model.Email;
            item.Expertise = model.Expertise;
            item.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Reviewers.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var item = await _context.Reviewers.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Reviewers.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}