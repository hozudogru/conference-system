using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommitteeMembersController : Controller
    {
        private readonly AppDbContext _context;

        public CommitteeMembersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.CommitteeMembers
                .Include(x => x.Conference)
                .OrderBy(x => x.CommitteeType)
                .ThenBy(x => x.SortOrder)
                .ThenBy(x => x.FullName)
                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            await LoadConferences();
            LoadCommitteeTypes();
            return View(new CommitteeMember
            {
                IsActive = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommitteeMember model)
        {
            if (!ModelState.IsValid)
            {
                await LoadConferences();
                LoadCommitteeTypes();
                return View(model);
            }

            _context.CommitteeMembers.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var item = await _context.CommitteeMembers.FindAsync(id);
            if (item == null)
                return NotFound();

            await LoadConferences();
            LoadCommitteeTypes();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CommitteeMember model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadConferences();
                LoadCommitteeTypes();
                return View(model);
            }

            var item = await _context.CommitteeMembers.FindAsync(id);
            if (item == null)
                return NotFound();

            item.ConferenceId = model.ConferenceId;
            item.CommitteeType = model.CommitteeType;
            item.FullName = model.FullName;
            item.Title = model.Title;
            item.Institution = model.Institution;
            item.SortOrder = model.SortOrder;
            item.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.CommitteeMembers
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
            var item = await _context.CommitteeMembers.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.CommitteeMembers.Remove(item);
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

        private void LoadCommitteeTypes()
        {
            var committeeTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Bilim Kurulu", Text = "Bilim Kurulu" },
                new SelectListItem { Value = "Düzenleme Kurulu", Text = "Düzenleme Kurulu" },
                new SelectListItem { Value = "Onur Kurulu", Text = "Onur Kurulu" },
                new SelectListItem { Value = "Organizasyon Komitesi", Text = "Organizasyon Komitesi" }
            };

            ViewBag.CommitteeTypes = committeeTypes;
        }
    }
}