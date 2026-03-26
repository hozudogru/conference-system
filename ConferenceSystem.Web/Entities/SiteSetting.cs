using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class SiteSetting
    {
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Key { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}