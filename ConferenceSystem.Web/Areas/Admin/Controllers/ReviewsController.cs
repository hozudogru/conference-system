using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Reviews
                .Include(x => x.ReviewAssignment)
                    .ThenInclude(x => x!.Submission)
                .Include(x => x.ReviewAssignment)
                    .ThenInclude(x => x!.Reviewer)
                .OrderByDescending(x => x.SubmittedAt)
                .ToListAsync();

            return View(items);
        }

        public async Task<IActionResult> Create(long reviewAssignmentId)
        {
            var assignment = await _context.ReviewAssignments
                .Include(x => x.Submission)
                .Include(x => x.Reviewer)
                .FirstOrDefaultAsync(x => x.Id == reviewAssignmentId);

            if (assignment == null)
                return NotFound();

            ViewBag.AssignmentInfo = $"{assignment.Submission?.Title} / {assignment.Reviewer?.FullName}";
            ViewBag.AssignmentId = assignment.Id;

            LoadRecommendations("Revise");

            return View(new Review
            {
                ReviewAssignmentId = assignment.Id,
                Recommendation = "Revise"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review model)
        {
            var assignment = await _context.ReviewAssignments
                .Include(x => x.Submission)
                .Include(x => x.Reviewer)
                .FirstOrDefaultAsync(x => x.Id == model.ReviewAssignmentId);

            if (assignment == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.AssignmentInfo = $"{assignment.Submission?.Title} / {assignment.Reviewer?.FullName}";
                ViewBag.AssignmentId = assignment.Id;
                LoadRecommendations(model.Recommendation);
                return View(model);
            }

            model.SubmittedAt = DateTime.Now;
            _context.Reviews.Add(model);

            assignment.Status = "Completed";

            if (assignment.Submission != null)
            {
                assignment.Submission.Status = "Reviewed";
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Detail", "Submissions", new { area = "Admin", id = assignment.SubmissionId });
        }

        public async Task<IActionResult> Detail(long id)
        {
            var item = await _context.Reviews
                .Include(x => x.ReviewAssignment)
                    .ThenInclude(x => x!.Submission)
                .Include(x => x.ReviewAssignment)
                    .ThenInclude(x => x!.Reviewer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }
        public async Task<IActionResult> ExportExcel()
        {
            var reviews = await _context.Reviews
                .Include(x => x.ReviewAssignment)
                    .ThenInclude(x => x!.Submission)
                .Include(x => x.ReviewAssignment)
                    .ThenInclude(x => x!.Reviewer)
                .OrderByDescending(x => x.SubmittedAt)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Reviews");

            worksheet.Cell(1, 1).Value = "Başvuru No";
            worksheet.Cell(1, 2).Value = "Başlık";
            worksheet.Cell(1, 3).Value = "Hakem";
            worksheet.Cell(1, 4).Value = "Özgünlük";
            worksheet.Cell(1, 5).Value = "Yöntem";
            worksheet.Cell(1, 6).Value = "Uygunluk";
            worksheet.Cell(1, 7).Value = "Yazım";
            worksheet.Cell(1, 8).Value = "Öneri";
            worksheet.Cell(1, 9).Value = "Tarih";

            worksheet.Range(1, 1, 1, 9).Style.Font.Bold = true;

            var row = 2;
            foreach (var item in reviews)
            {
                worksheet.Cell(row, 1).Value = item.ReviewAssignment?.Submission?.SubmissionNumber ?? "";
                worksheet.Cell(row, 2).Value = item.ReviewAssignment?.Submission?.Title ?? "";
                worksheet.Cell(row, 3).Value = item.ReviewAssignment?.Reviewer?.FullName ?? "";
                worksheet.Cell(row, 4).Value = item.OriginalityScore;
                worksheet.Cell(row, 5).Value = item.MethodScore;
                worksheet.Cell(row, 6).Value = item.RelevanceScore;
                worksheet.Cell(row, 7).Value = item.WritingScore;
                worksheet.Cell(row, 8).Value = item.Recommendation ?? "";
                worksheet.Cell(row, 9).Value = item.SubmittedAt.ToString("dd.MM.yyyy HH:mm");
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"reviews_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }

        private void LoadRecommendations(string? selectedValue = null)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = "Accept", Text = "Accept" },
                new SelectListItem { Value = "Revise", Text = "Revise" },
                new SelectListItem { Value = "Reject", Text = "Reject" }
            };

            ViewBag.Recommendations = new SelectList(items, "Value", "Text", selectedValue);
        }
    }
}