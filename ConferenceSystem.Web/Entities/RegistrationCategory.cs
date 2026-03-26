using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class RegistrationCategory
    {
        public long Id { get; set; }

        public long ConferenceId { get; set; }
        public Conference? Conference { get; set; }

        [Required]
        [StringLength(150)]
        public string CategoryName { get; set; } = "";

        [StringLength(300)]
        public string Description { get; set; } = "";

        public decimal Fee { get; set; }

        [StringLength(20)]
        public string Currency { get; set; } = "₺";

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
