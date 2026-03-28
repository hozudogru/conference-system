using ClosedXML.Excel;
using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConferenceSystem.Web.Services;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubmissionsController : Controller
    {
        
        private readonly AppDbContext _context;

        public SubmissionsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? status = null)
        {
            var query = _context.Submissions
                .Include(x => x.Conference)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(x => x.Status == status);
                ViewBag.CurrentStatus = status;
            }
            else
            {
                ViewBag.CurrentStatus = "";
            }

            var list = await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Detail(long id)
        {
            var item = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            var assignments = await _context.ReviewAssignments
                .Include(x => x.Reviewer)
                .Where(x => x.SubmissionId == id)
                .OrderBy(x => x.AssignedAt)
                .ToListAsync();

            var assignmentIds = assignments.Select(x => x.Id).ToList();

            var reviews = await _context.Reviews
                .Include(x => x.ReviewAssignment)
                    .ThenInclude(x => x!.Reviewer)
                .Where(x => assignmentIds.Contains(x.ReviewAssignmentId))
                .OrderBy(x => x.SubmittedAt)
                .ToListAsync();

            var decisions = await _context.EditorialDecisions
                .Where(x => x.SubmissionId == id)
                .OrderByDescending(x => x.DecidedAt)
                .ToListAsync();

            var revisionFiles = await _context.RevisionFiles
                .Where(x => x.SubmissionId == id)
                .OrderByDescending(x => x.UploadedAt)
                .ToListAsync();

            var latestDecision = decisions.FirstOrDefault();

            ViewBag.Assignments = assignments;
            ViewBag.Reviews = reviews;
            ViewBag.Decisions = decisions;
            ViewBag.RevisionFiles = revisionFiles;
            ViewBag.LatestDecision = latestDecision;

            ViewBag.AssignmentCount = assignments.Count;
            ViewBag.CompletedReviewCount = reviews.Count;
            ViewBag.PendingReviewCount = Math.Max(assignments.Count - reviews.Count, 0);
            ViewBag.HasRevisionFile = revisionFiles.Any();
            ViewBag.HasDecision = latestDecision != null;

            return View(item);
        }

        public async Task<IActionResult> EditStatus(long id)
        {
            var item = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            LoadStatusList(item.Status);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(long id, Submission model)
        {
            if (id != model.Id)
                return NotFound();

            var item = await _context.Submissions.FindAsync(id);
            if (item == null)
                return NotFound();

            item.Status = model.Status;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Detail), new { id = item.Id });
        }

        public async Task<IActionResult> ExportExcel(string? status = null)
        {
            var query = _context.Submissions
                .Include(x => x.Conference)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(x => x.Status == status);
            }

            var submissions = await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Submissions");

            worksheet.Cell(1, 1).Value = "Konferans";
            worksheet.Cell(1, 2).Value = "Başvuru No";
            worksheet.Cell(1, 3).Value = "Başlık";
            worksheet.Cell(1, 4).Value = "Yazar";
            worksheet.Cell(1, 5).Value = "E-posta";
            worksheet.Cell(1, 6).Value = "Kurum";
            worksheet.Cell(1, 7).Value = "Durum";
            worksheet.Cell(1, 8).Value = "Tarih";
            worksheet.Cell(1, 9).Value = "Dosya Yolu";

            var headerRange = worksheet.Range(1, 1, 1, 9);
            headerRange.Style.Font.Bold = true;

            int row = 2;
            foreach (var item in submissions)
            {
                worksheet.Cell(row, 1).Value = item.Conference?.Name ?? "";
                worksheet.Cell(row, 2).Value = item.SubmissionNumber ?? "";
                worksheet.Cell(row, 3).Value = item.Title ?? "";
                worksheet.Cell(row, 4).Value = item.AuthorFullName ?? "";
                worksheet.Cell(row, 5).Value = item.Email ?? "";
                worksheet.Cell(row, 6).Value = item.Institution ?? "";
                worksheet.Cell(row, 7).Value = item.Status ?? "";
                worksheet.Cell(row, 8).Value = item.CreatedAt.ToString("dd.MM.yyyy HH:mm");
                worksheet.Cell(row, 9).Value = item.FilePath ?? "";
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = string.IsNullOrWhiteSpace(status)
                ? $"submissions_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                : $"submissions_{status.Replace(" ", "_").ToLower()}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        private void LoadStatusList(string? selectedValue = null)
        {
            var items = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
            {
                new() { Value = "Pending", Text = "Pending" },
                new() { Value = "Under Review", Text = "Under Review" },
                new() { Value = "Reviewed", Text = "Reviewed" },
                new() { Value = "Accepted", Text = "Accepted" },
                new() { Value = "Revision Required", Text = "Revision Required" },
                new() { Value = "Rejected", Text = "Rejected" },
                new() { Value = "Revised Submitted", Text = "Revised Submitted" }
            };

            ViewBag.StatusList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(items, "Value", "Text", selectedValue);
        }
    }
}