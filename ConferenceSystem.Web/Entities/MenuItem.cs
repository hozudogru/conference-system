using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Url { get; set; } = string.Empty;

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public bool OpenInNewTab { get; set; } = false;
        [StringLength(100)]
        public string MenuGroup { get; set; } = "";

        [StringLength(20)]
        public string MenuLocation { get; set; } = "Public";
        // Public / Admin
    }
}