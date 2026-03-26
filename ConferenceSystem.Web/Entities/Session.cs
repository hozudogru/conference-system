using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class Session
    {
        public long Id { get; set; }

        [Required]
        public long ConferenceId { get; set; }

        [StringLength(200)]
        public string Title { get; set; } = "";

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [StringLength(100)]
        public string Room { get; set; } = "";
        
        [StringLength(200)]
        public string ChairName { get; set; } = "";
        public Conference? Conference { get; set; }
        public List<SessionItem> Items { get; set; } = new();
    }
}