using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using ConferenceSystem.Web.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ScheduleController : Controller
    {
        private readonly AppDbContext _context;

        public ScheduleController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(long conferenceId)
        {
            var conference = await _context.Conferences.FindAsync(conferenceId);
            if (conference == null)
                return NotFound();

            var sessions = await _context.Sessions
                .Include(x => x.Items.OrderBy(i => i.Order))
                    .ThenInclude(x => x.Submission)
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.StartTime)
                .ThenBy(x => x.Room)
                .ToListAsync();

            var allSubmissions = await _context.Submissions
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.SubmissionNumber)
                .ThenBy(x => x.Id)
                .ToListAsync();

            var assignedSubmissionIds = sessions
                .SelectMany(x => x.Items)
                .Select(x => x.SubmissionId)
                .Distinct()
                .ToHashSet();

            ViewBag.Conference = conference;
            ViewBag.ConferenceId = conferenceId;
            ViewBag.AllSubmissions = allSubmissions;
            ViewBag.AssignedSubmissionIds = assignedSubmissionIds;

            return View(sessions);
        }

        public async Task<IActionResult> Generate(long conferenceId, int roomCount = 2, int itemPerSession = 4)
        {
            var accepted = await _context.Submissions
                .Where(x => x.ConferenceId == conferenceId &&
                            x.Status != null &&
                            x.Status.Trim().ToLower() == "accepted")
                .OrderBy(x => x.Id)
                .ToListAsync();

            if (!accepted.Any())
                return Content($"Accepted bildiri yok. conferenceId={conferenceId}");

            var sessionIds = await _context.Sessions
                .Where(x => x.ConferenceId == conferenceId)
                .Select(x => x.Id)
                .ToListAsync();

            var oldItems = _context.SessionItems.Where(x => sessionIds.Contains(x.SessionId));
            _context.SessionItems.RemoveRange(oldItems);

            var oldSessions = _context.Sessions.Where(x => x.ConferenceId == conferenceId);
            _context.Sessions.RemoveRange(oldSessions);

            await _context.SaveChangesAsync();

            var baseDate = DateTime.Today.AddHours(9);
            int totalSessionsNeeded = (int)Math.Ceiling((double)accepted.Count / itemPerSession);

            int waveCount = (int)Math.Ceiling((double)totalSessionsNeeded / roomCount);
            int acceptedIndex = 0;
            int sessionNumber = 1;

            for (int wave = 0; wave < waveCount; wave++)
            {
                var waveStart = baseDate.AddMinutes(wave * 70);

                for (int room = 1; room <= roomCount; room++)
                {
                    if (acceptedIndex >= accepted.Count)
                        break;

                    var session = new Session
                    {
                        ConferenceId = conferenceId,
                        Title = $"Session {sessionNumber}",
                        Room = $"Hall {room}",
                        ChairName = "",
                        StartTime = waveStart,
                        EndTime = waveStart.AddMinutes(60)
                    };

                    _context.Sessions.Add(session);
                    await _context.SaveChangesAsync();

                    int order = 1;
                    for (int j = 0; j < itemPerSession && acceptedIndex < accepted.Count; j++)
                    {
                        _context.SessionItems.Add(new SessionItem
                        {
                            SessionId = session.Id,
                            SubmissionId = accepted[acceptedIndex].Id,
                            Order = order++,
                            PresentationMinutes = 15
                        });

                        acceptedIndex++;
                    }

                    await _context.SaveChangesAsync();
                    sessionNumber++;
                }
            }

            return RedirectToAction(nameof(Index), new { conferenceId });
        }

        [HttpGet]
        public async Task<IActionResult> Import(long conferenceId)
        {
            var conference = await _context.Conferences.FindAsync(conferenceId);
            if (conference == null)
                return NotFound();

            ViewBag.Conference = conference;
            ViewBag.ConferenceId = conferenceId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(long conferenceId, IFormFile? excelFile)
        {
            var conference = await _context.Conferences.FindAsync(conferenceId);
            if (conference == null)
                return NotFound();

            ViewBag.Conference = conference;
            ViewBag.ConferenceId = conferenceId;

            if (excelFile == null || excelFile.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen Excel dosyası seçiniz.");
                return View();
            }

            var rows = new List<ScheduleImportRowViewModel>();

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                stream.Position = 0;

                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);

                var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;

                for (int row = 2; row <= lastRow; row++)
                {
                    var sessionTitle = worksheet.Cell(row, 1).GetString().Trim();
                    var room = worksheet.Cell(row, 2).GetString().Trim();
                    var chairName = worksheet.Cell(row, 3).GetString().Trim();
                    var startTimeText = worksheet.Cell(row, 4).GetString().Trim();
                    var endTimeText = worksheet.Cell(row, 5).GetString().Trim();
                    var submissionNumber = worksheet.Cell(row, 6).GetString().Trim();
                    var orderText = worksheet.Cell(row, 7).GetString().Trim();
                    var presentationMinutesText = worksheet.Cell(row, 8).GetString().Trim();

                    if (string.IsNullOrWhiteSpace(sessionTitle) || string.IsNullOrWhiteSpace(submissionNumber))
                        continue;

                    if (!DateTime.TryParse(startTimeText, out var startTime))
                    {
                        ModelState.AddModelError("", $"Satır {row}: StartTime hatalı.");
                        return View();
                    }

                    if (!DateTime.TryParse(endTimeText, out var endTime))
                    {
                        ModelState.AddModelError("", $"Satır {row}: EndTime hatalı.");
                        return View();
                    }

                    if (!int.TryParse(orderText, out var order))
                    {
                        ModelState.AddModelError("", $"Satır {row}: Order hatalı.");
                        return View();
                    }

                    if (!int.TryParse(presentationMinutesText, out var presentationMinutes))
                        presentationMinutes = 15;

                    rows.Add(new ScheduleImportRowViewModel
                    {
                        SessionTitle = sessionTitle,
                        Room = room,
                        ChairName = chairName,
                        StartTime = startTime,
                        EndTime = endTime,
                        SubmissionNumber = submissionNumber,
                        Order = order,
                        PresentationMinutes = presentationMinutes
                    });
                }
            }

            if (!rows.Any())
            {
                ModelState.AddModelError("", "Excel içinde içe aktarılacak veri bulunamadı.");
                return View();
            }

            var existingSessionIds = await _context.Sessions
                .Where(x => x.ConferenceId == conferenceId)
                .Select(x => x.Id)
                .ToListAsync();

            var oldItems = _context.SessionItems.Where(x => existingSessionIds.Contains(x.SessionId));
            _context.SessionItems.RemoveRange(oldItems);

            var oldSessions = _context.Sessions.Where(x => x.ConferenceId == conferenceId);
            _context.Sessions.RemoveRange(oldSessions);

            await _context.SaveChangesAsync();

            var sessionMap = new Dictionary<string, long>();

            foreach (var row in rows)
            {
                var sessionKey = $"{row.SessionTitle}|{row.Room}|{row.StartTime:yyyyMMddHHmm}|{row.EndTime:yyyyMMddHHmm}";

                if (!sessionMap.ContainsKey(sessionKey))
                {
                    var session = new Session
                    {
                        ConferenceId = conferenceId,
                        Title = row.SessionTitle,
                        Room = row.Room,
                        ChairName = row.ChairName,
                        StartTime = row.StartTime,
                        EndTime = row.EndTime
                    };

                    _context.Sessions.Add(session);
                    await _context.SaveChangesAsync();

                    sessionMap[sessionKey] = session.Id;
                }

                var submission = await _context.Submissions
                    .FirstOrDefaultAsync(x => x.ConferenceId == conferenceId && x.SubmissionNumber == row.SubmissionNumber);

                if (submission == null)
                    continue;

                _context.SessionItems.Add(new SessionItem
                {
                    SessionId = sessionMap[sessionKey],
                    SubmissionId = submission.Id,
                    Order = row.Order,
                    PresentationMinutes = row.PresentationMinutes
                });
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Program Excel dosyasından başarıyla içe aktarıldı.";
            return RedirectToAction(nameof(Index), new { conferenceId });
        }

        [HttpGet]
        public async Task<IActionResult> EditSession(long id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return NotFound();

            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSession(long id, Session model)
        {
            if (id != model.Id)
                return NotFound();

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return NotFound();

            session.Title = model.Title;
            session.Room = model.Room;
            session.ChairName = model.ChairName;
            session.StartTime = model.StartTime;
            session.EndTime = model.EndTime;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { conferenceId = session.ConferenceId });
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody] List<ScheduleMoveDto> items)
        {
            if (items == null || !items.Any())
                return Json(new { success = false, message = "Kaydedilecek veri yok." });

            var targetSessionIds = items
                .Where(x => x.TargetSessionId > 0)
                .Select(x => x.TargetSessionId)
                .Distinct()
                .ToList();

            var sessions = await _context.Sessions
                .Where(x => targetSessionIds.Contains(x.Id))
                .ToListAsync();

            if (!sessions.Any())
                return Json(new { success = false, message = "Oturum bulunamadı." });

            var conferenceId = sessions.First().ConferenceId;
            if (sessions.Any(x => x.ConferenceId != conferenceId))
                return Json(new { success = false, message = "Farklı konferans oturumları birlikte gönderilemez." });

            var existingSessionIds = await _context.Sessions
                .Where(x => x.ConferenceId == conferenceId)
                .Select(x => x.Id)
                .ToListAsync();

            var existingItems = await _context.SessionItems
                .Where(x => existingSessionIds.Contains(x.SessionId))
                .ToListAsync();

            var existingById = existingItems.ToDictionary(x => x.Id, x => x);

            var submittedExistingIds = items
                .Where(x => x.SessionItemId.HasValue && x.SessionItemId.Value > 0)
                .Select(x => x.SessionItemId!.Value)
                .ToHashSet();

            var submittedSubmissionIds = items
                .Where(x => x.SubmissionId > 0)
                .Select(x => x.SubmissionId)
                .ToHashSet();

            var toDelete = existingItems
                .Where(x => !submittedExistingIds.Contains(x.Id) && !submittedSubmissionIds.Contains(x.SubmissionId))
                .ToList();

            if (toDelete.Any())
                _context.SessionItems.RemoveRange(toDelete);

            foreach (var item in items)
            {
                if (item.SessionItemId.HasValue && item.SessionItemId.Value > 0 && existingById.TryGetValue(item.SessionItemId.Value, out var existing))
                {
                    existing.SessionId = item.TargetSessionId;
                    existing.Order = item.Order;
                    existing.PresentationMinutes = item.PresentationMinutes;
                    continue;
                }

                var sameSubmissionAlreadyExists = await _context.SessionItems
                    .AnyAsync(x => existingSessionIds.Contains(x.SessionId) && x.SubmissionId == item.SubmissionId);

                if (sameSubmissionAlreadyExists)
                    continue;

                _context.SessionItems.Add(new SessionItem
                {
                    SessionId = item.TargetSessionId,
                    SubmissionId = item.SubmissionId,
                    Order = item.Order,
                    PresentationMinutes = item.PresentationMinutes
                });
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult DownloadTemplate()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("ScheduleTemplate");

            worksheet.Cell(1, 1).Value = "SessionTitle";
            worksheet.Cell(1, 2).Value = "Room";
            worksheet.Cell(1, 3).Value = "ChairName";
            worksheet.Cell(1, 4).Value = "StartTime";
            worksheet.Cell(1, 5).Value = "EndTime";
            worksheet.Cell(1, 6).Value = "SubmissionNumber";
            worksheet.Cell(1, 7).Value = "Order";
            worksheet.Cell(1, 8).Value = "PresentationMinutes";

            worksheet.Cell(2, 1).Value = "Session 1";
            worksheet.Cell(2, 2).Value = "Hall 1";
            worksheet.Cell(2, 3).Value = "Prof. Dr. X";
            worksheet.Cell(2, 4).Value = "2026-06-10 09:00";
            worksheet.Cell(2, 5).Value = "2026-06-10 10:00";
            worksheet.Cell(2, 6).Value = "CONF2026-001";
            worksheet.Cell(2, 7).Value = 1;
            worksheet.Cell(2, 8).Value = 15;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "schedule_template.xlsx");
        }
    }

    public class ScheduleMoveDto
    {
        public long? SessionItemId { get; set; }
        public long SubmissionId { get; set; }
        public long TargetSessionId { get; set; }
        public int Order { get; set; }
        public int PresentationMinutes { get; set; }
    }
}