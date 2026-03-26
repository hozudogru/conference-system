using ConferenceSystem.Web.Entities;

namespace ConferenceSystem.Web.ViewModels
{
    public class HomePageViewModel
    {
        public Conference? ActiveConference { get; set; }

        public HomePageSetting? HomePageSetting { get; set; }

        public Dictionary<string, string> ThemeSettings { get; set; } = new();

        public List<Announcement> Announcements { get; set; } = new();

        public List<ImportantDate> ImportantDates { get; set; } = new();

        public List<CommitteeMember> CommitteeMembers { get; set; } = new();

        public List<RegistrationCategory> RegistrationCategories { get; set; } = new();

        public List<Speaker> Speakers { get; set; } = new();
    }
}
