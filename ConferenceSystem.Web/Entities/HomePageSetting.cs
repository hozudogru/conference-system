using System.ComponentModel.DataAnnotations;

namespace ConferenceSystem.Web.Entities
{
    public class HomePageSetting
    {
        public long Id { get; set; }

        public long ConferenceId { get; set; }
        public Conference? Conference { get; set; }

        [StringLength(200)]
        public string HeroBadgeText { get; set; } = "";

        [StringLength(500)]
        public string HeroTitle { get; set; } = "";

        public string HeroDescription { get; set; } = "";

        [StringLength(100)]
        public string HeroPrimaryButtonText { get; set; } = "Özet Gönder";

        [StringLength(250)]
        public string HeroPrimaryButtonUrl { get; set; } = "/Submission/Create";

        [StringLength(100)]
        public string HeroSecondaryButtonText { get; set; } = "Programı İncele";

        [StringLength(250)]
        public string HeroSecondaryButtonUrl { get; set; } = "/Program";

        [StringLength(300)]
        public string PosterImageUrl { get; set; } = "";

        [StringLength(100)]
        public string HighlightBox1Title { get; set; } = "Özet Gönderim Başlangıcı";

        [StringLength(200)]
        public string HighlightBox1Value { get; set; } = "";

        [StringLength(100)]
        public string HighlightBox2Title { get; set; } = "Kongre Tarihi";

        [StringLength(200)]
        public string HighlightBox2Value { get; set; } = "";

        [StringLength(100)]
        public string HighlightBox3Title { get; set; } = "Tam Metin";

        [StringLength(200)]
        public string HighlightBox3Value { get; set; } = "";

        [StringLength(100)]
        public string ContactTitle { get; set; } = "İletişim";

        public string ContactHtml { get; set; } = "";

        public bool ShowAnnouncements { get; set; } = true;
        public bool ShowImportantDates { get; set; } = true;
        public bool ShowRegistrationCategories { get; set; } = true;
        public bool ShowCommitteeMembers { get; set; } = false;
        public bool ShowSpeakers { get; set; } = true;
        public bool ShowContact { get; set; } = true;
    }
}
