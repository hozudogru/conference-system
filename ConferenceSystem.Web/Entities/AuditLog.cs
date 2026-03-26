using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class AuditLog
    {
        public long Id { get; set; }

        [StringLength(100)]
        public string ActionType { get; set; } = "";

        [StringLength(100)]
        public string EntityType { get; set; } = "";

        public long? EntityId { get; set; }

        [StringLength(200)]
        public string PerformedBy { get; set; } = "";

        [StringLength(1000)]
        public string Description { get; set; } = "";

        [StringLength(100)]
        public string IpAddress { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}