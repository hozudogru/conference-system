using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImportantDatesController : Controller
    {
        private readonly AppDbContext _context;

        public ImportantDatesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.ImportantDates
                .Include(x => x.Conference)
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.DateValue)
                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            await LoadConferences();
            return View(new ImportantDate
            {
                DateValue = DateTime.Today,
                IsActive = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImportantDate model)
        {
            if (!ModelState.IsValid)
            {
                await LoadConferences();
                return View(model);
            }

            _context.ImportantDates.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var item = await _context.ImportantDates.FindAsync(id);
            if (item == null)
                return NotFound();

            await LoadConferences();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ImportantDate model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadConferences();
                return View(model);
            }

            var item = await _context.ImportantDates.FindAsync(id);
            if (item == null)
                return NotFound();

            item.ConferenceId = model.ConferenceId;
            item.Title = model.Title;
            item.DateValue = model.DateValue;
            item.Description = model.Description;
            item.SortOrder = model.SortOrder;
            item.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.ImportantDates
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var item = await _context.ImportantDates.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.ImportantDates.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadConferences()
        {
            var conferences = await _context.Conferences
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();

            ViewBag.Conferences = new SelectList(conferences, "Id", "Name");
        }
    }
}