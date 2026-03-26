using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using ConferenceSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EditorialDecisionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LetterService _letterService;
        private readonly EmailService _emailService;
        private readonly AuditService _auditService;
        public EditorialDecisionsController(
                 AppDbContext context,
                 LetterService letterService,
                 EmailService emailService,
                 AuditService auditService)
        {
            _context = context;
            _letterService = letterService;
            _emailService = emailService;
            _auditService = auditService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.EditorialDecisions
                .Include(x => x.Submission)
                .OrderByDescending(x => x.DecidedAt)
                .ToListAsync();

            return View(items);
        }
        public async Task<IActionResult> SendDecisionEmail(long submissionId)
        {
            var submission = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == submissionId);

            if (submission == null)
                return NotFound();

            var decision = await _context.EditorialDecisions
                .Where(x => x.SubmissionId == submissionId)
                .OrderByDescending(x => x.DecidedAt)
                .FirstOrDefaultAsync();

            if (decision == null)
                return NotFound("Bu bildiri için henüz karar kaydı bulunamadı.");

            if (string.IsNullOrWhiteSpace(submission.Email))
                return BadRequest("Yazar e-posta adresi bulunamadı.");

            var pdf = _letterService.GenerateDecisionLetter(
                submission,
                decision.DecisionType,
                decision.DecisionNote
            );

            var subject = decision.DecisionType switch
            {
                "Accepted" => "Submission Accepted",
                "Rejected" => "Submission Rejected",
                "Revision Required" => "Revision Required for Your Submission",
                _ => "Editorial Decision"
            };

            var body = decision.DecisionType switch
            {
                "Accepted" => $"""
Dear {submission.AuthorFullName},

We are pleased to inform you that your submission titled:

"{submission.Title}"

has been accepted.

Please find the attached decision letter.

Best regards,
Conference Committee
""",

                "Rejected" => $"""
Dear {submission.AuthorFullName},

We regret to inform you that your submission titled:

"{submission.Title}"

has been rejected.

Please find the attached decision letter.

Best regards,
Conference Committee
""",

                "Revision Required" => $"""
Dear {submission.AuthorFullName},

Your submission titled:

"{submission.Title}"

requires revision.

Please find the attached decision letter.

Best regards,
Conference Committee
""",

                _ => $"""
Dear {submission.AuthorFullName},

A decision has been made regarding your submission titled:

"{submission.Title}"

Please find the attached decision letter.

Best regards,
Conference Committee
"""
            };

            var fileName = $"{decision.DecisionType.Replace(" ", "_").ToLower()}_letter_{submissionId}.pdf";

            await _emailService.SendDecisionEmailAsync(
                submission.Email,
                submission.AuthorFullName,
                subject,
                body,
                pdf,
                fileName
            );

            TempData["Success"] = "Karar maili başarıyla gönderildi.";
            return RedirectToAction("Detail", "Submissions", new { area = "Admin", id = submissionId });
        }
        public async Task<IActionResult> Create(long submissionId)
        {
            var submission = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == submissionId);

            if (submission == null)
                return NotFound();

            ViewBag.SubmissionTitle = submission.Title;
            ViewBag.SubmissionId = submission.Id;

            LoadDecisionTypes("Revision Required");

            return View(new EditorialDecision
            {
                SubmissionId = submission.Id,
                DecisionType = "Revision Required"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EditorialDecision model)
        {
            var submission = await _context.Submissions.FindAsync(model.SubmissionId);
            if (submission == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.SubmissionTitle = submission.Title;
                ViewBag.SubmissionId = submission.Id;
                LoadDecisionTypes(model.DecisionType);
                return View(model);
            }

            model.DecidedAt = DateTime.Now;
            _context.EditorialDecisions.Add(model);

            submission.Status = model.DecisionType switch
            {
                "Accepted" => "Accepted",
                "Rejected" => "Rejected",
                _ => "Revision Required"
            };

            await _context.SaveChangesAsync();

            if (model.DecisionType == "Accepted")
            {
                var exists = await _context.Proceedings
                    .AnyAsync(x => x.SubmissionId == model.SubmissionId);

                if (!exists)
                {
                    var proceeding = new ConferenceSystem.Web.Entities.Proceeding
                    {
                        SubmissionId = model.SubmissionId,
                        PublishedAt = DateTime.Now
                    };

                    _context.Proceedings.Add(proceeding);
                    await _context.SaveChangesAsync();
                }
            }

            await _auditService.LogAsync(
                "Create",
                "EditorialDecision",
                model.Id,
                "Admin",
                $"SubmissionId={model.SubmissionId} için nihai karar verildi. Decision={model.DecisionType}");

            return RedirectToAction("Detail", "Submissions", new { area = "Admin", id = model.SubmissionId });
        }

        public async Task<IActionResult> Detail(long id)
        {
            var item = await _context.EditorialDecisions
                .Include(x => x.Submission)
                .ThenInclude(x => x!.Conference)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        public async Task<IActionResult> DownloadLetter(long submissionId)
        {
            var submission = await _context.Submissions
                .Include(x => x.Conference)
                .FirstOrDefaultAsync(x => x.Id == submissionId);

            if (submission == null)
                return NotFound();

            var decision = await _context.EditorialDecisions
                .Where(x => x.SubmissionId == submissionId)
                .OrderByDescending(x => x.DecidedAt)
                .FirstOrDefaultAsync();

            if (decision == null)
                return NotFound("Bu bildiri için henüz karar kaydı bulunamadı.");

            var pdf = _letterService.GenerateDecisionLetter(
                submission,
                decision.DecisionType,
                decision.DecisionNote
            );

            var fileName = $"{decision.DecisionType.Replace(" ", "_").ToLower()}_letter_{submissionId}.pdf";

            return File(pdf, "application/pdf", fileName);
        }
        public async Task<IActionResult> ExportExcel()
        {
            var decisions = await _context.EditorialDecisions
                .Include(x => x.Submission)
                .OrderByDescending(x => x.DecidedAt)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Decisions");

            worksheet.Cell(1, 1).Value = "Başvuru No";
            worksheet.Cell(1, 2).Value = "Başlık";
            worksheet.Cell(1, 3).Value = "Yazar";
            worksheet.Cell(1, 4).Value = "E-posta";
            worksheet.Cell(1, 5).Value = "Durum";
            worksheet.Cell(1, 6).Value = "Nihai Karar";
            worksheet.Cell(1, 7).Value = "Karar Notu";
            worksheet.Cell(1, 8).Value = "Karar Tarihi";

            worksheet.Range(1, 1, 1, 8).Style.Font.Bold = true;

            var row = 2;
            foreach (var item in decisions)
            {
                worksheet.Cell(row, 1).Value = item.Submission?.SubmissionNumber ?? "";
                worksheet.Cell(row, 2).Value = item.Submission?.Title ?? "";
                worksheet.Cell(row, 3).Value = item.Submission?.AuthorFullName ?? "";
                worksheet.Cell(row, 4).Value = item.Submission?.Email ?? "";
                worksheet.Cell(row, 5).Value = item.Submission?.Status ?? "";
                worksheet.Cell(row, 6).Value = item.DecisionType ?? "";
                worksheet.Cell(row, 7).Value = item.DecisionNote ?? "";
                worksheet.Cell(row, 8).Value = item.DecidedAt.ToString("dd.MM.yyyy HH:mm");
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"editorial_decisions_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        private void LoadDecisionTypes(string? selectedValue = null)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = "Accepted", Text = "Accepted" },
                new SelectListItem { Value = "Revision Required", Text = "Revision Required" },
                new SelectListItem { Value = "Rejected", Text = "Rejected" }
            };

            ViewBag.DecisionTypes = new SelectList(items, "Value", "Text", selectedValue);
        }
    }
}