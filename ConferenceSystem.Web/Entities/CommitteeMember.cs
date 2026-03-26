using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class CommitteeMember
    {
        public long Id { get; set; }

        public long ConferenceId { get; set; }

        [Required]
        [StringLength(100)]
        public string CommitteeType { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = "";

        [StringLength(150)]
        public string Title { get; set; } = "";

        [StringLength(250)]
        public string Institution { get; set; } = "";

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public Conference? Conference { get; set; }
    }
}