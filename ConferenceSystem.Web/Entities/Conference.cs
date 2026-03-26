using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class Conference
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string ShortName { get; set; } = "";

        public string Description { get; set; } = "";

        public string Location { get; set; } = "";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string WebsiteUrl { get; set; } = "";

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        [StringLength(300)]
        public string LogoPath { get; set; } = "";

        [StringLength(200)]
        public string PresidentName { get; set; } = "";

        [StringLength(1000)]
        public string OrganizingCommitteeInfo { get; set; } = "";
    }
}