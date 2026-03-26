using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;

namespace ConferenceSystem.Web.Services
{
    public class AuditService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(
            string actionType,
            string entityType,
            long? entityId,
            string performedBy,
            string description)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";

            var log = new AuditLog
            {
                ActionType = actionType,
                EntityType = entityType,
                EntityId = entityId,
                PerformedBy = performedBy,
                Description = description,
                IpAddress = ipAddress,
                CreatedAt = DateTime.Now
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}