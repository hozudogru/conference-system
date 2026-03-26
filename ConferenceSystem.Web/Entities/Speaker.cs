using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class Speaker
    {
        public long Id { get; set; }

        public long ConferenceId { get; set; }
        public Conference? Conference { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = "";

        [StringLength(150)]
        public string Title { get; set; } = "";

        [StringLength(250)]
        public string Institution { get; set; } = "";

        [StringLength(300)]
        public string ImageUrl { get; set; } = "";

        public string Bio { get; set; } = "";

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
