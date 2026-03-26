using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpeakersController : Controller
    {
        private readonly AppDbContext _context;

        public SpeakersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(long? conferenceId)
        {
            var query = _context.Speakers.AsQueryable();
            if (conferenceId.HasValue)
                query = query.Where(x => x.ConferenceId == conferenceId.Value);

            var items = await query.OrderBy(x => x.SortOrder).ToListAsync();
            ViewBag.Conferences = await _context.Conferences.OrderByDescending(x => x.StartDate).ToListAsync();
            ViewBag.SelectedConferenceId = conferenceId;
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create(long? conferenceId)
        {
            ViewBag.Conferences = await _context.Conferences.OrderByDescending(x => x.StartDate).ToListAsync();
            return View(new Speaker { ConferenceId = conferenceId ?? 0, IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speaker model)
        {
            ViewBag.Conferences = await _context.Conferences.OrderByDescending(x => x.StartDate).ToListAsync();
            if (!ModelState.IsValid)
                return View(model);

            _context.Speakers.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Konuşmacı eklendi.";
            return RedirectToAction(nameof(Index), new { conferenceId = model.ConferenceId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var model = await _context.Speakers.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return NotFound();
            ViewBag.Conferences = await _context.Conferences.OrderByDescending(x => x.StartDate).ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Speaker model)
        {
            ViewBag.Conferences = await _context.Conferences.OrderByDescending(x => x.StartDate).ToListAsync();
            if (!ModelState.IsValid)
                return View(model);

            _context.Speakers.Update(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Konuşmacı güncellendi.";
            return RedirectToAction(nameof(Index), new { conferenceId = model.ConferenceId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var model = await _context.Speakers.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var model = await _context.Speakers.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return NotFound();
            var conferenceId = model.ConferenceId;
            _context.Speakers.Remove(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Konuşmacı silindi.";
            return RedirectToAction(nameof(Index), new { conferenceId });
        }
    }
}
