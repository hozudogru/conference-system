using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ConferencesController : Controller
    {
        private readonly AppDbContext _context;

        public ConferencesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Conferences
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();

            return View(list);
        }

        public IActionResult Create()
        {
            return View(new Conference
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                IsActive = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Conference model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.CreatedAt = DateTime.Now;

            _context.Conferences.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var item = await _context.Conferences.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Conference model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var item = await _context.Conferences.FindAsync(id);
            if (item == null)
                return NotFound();

            item.Name = model.Name;
            item.ShortName = model.ShortName;
            item.Description = model.Description;
            item.Location = model.Location;
            item.StartDate = model.StartDate;
            item.EndDate = model.EndDate;
            item.WebsiteUrl = model.WebsiteUrl;
            item.IsActive = model.IsActive;
            item.LogoPath = model.LogoPath;
            item.PresidentName = model.PresidentName;
            item.OrganizingCommitteeInfo = model.OrganizingCommitteeInfo;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Conferences.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var item = await _context.Conferences.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Conferences.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}