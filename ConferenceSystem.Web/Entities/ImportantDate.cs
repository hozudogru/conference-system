using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class ImportantDate
    {
        public long Id { get; set; }

        public long ConferenceId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = "";

        public DateTime DateValue { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = "";

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public Conference? Conference { get; set; }
    }
}