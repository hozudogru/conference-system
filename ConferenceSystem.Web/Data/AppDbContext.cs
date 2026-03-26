using ConferenceSystem.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ContentPage> Pages { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ImportantDate> ImportantDates { get; set; }
        public DbSet<CommitteeMember> CommitteeMembers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<ReviewAssignment> ReviewAssignments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<EditorialDecision> EditorialDecisions { get; set; }
        public DbSet<RevisionFile> RevisionFiles { get; set; }
        public DbSet<MagicLoginToken> MagicLoginTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Proceeding> Proceedings { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionItem> SessionItems { get; set; }
        public DbSet<SmtpSetting> SmtpSettings { get; set; }
        public DbSet<HomePageSetting> HomePageSettings { get; set; }
        public DbSet<RegistrationCategory> RegistrationCategories { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SessionItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Session)
                      .WithMany(x => x.Items)
                      .HasForeignKey(x => x.SessionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Submission)
                      .WithMany()
                      .HasForeignKey(x => x.SubmissionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (string.IsNullOrWhiteSpace(tableName))
                    continue;

                entity.SetTableName(tableName.ToLowerInvariant());

                foreach (var property in entity.GetProperties())
                {
                    if (!string.IsNullOrWhiteSpace(property.Name))
                        property.SetColumnName(property.Name.ToLowerInvariant());
                }

                foreach (var key in entity.GetKeys())
                {
                    var keyName = key.GetName();
                    if (!string.IsNullOrWhiteSpace(keyName))
                        key.SetName(keyName.ToLowerInvariant());
                }

                foreach (var fk in entity.GetForeignKeys())
                {
                    var fkName = fk.GetConstraintName();
                    if (!string.IsNullOrWhiteSpace(fkName))
                        fk.SetConstraintName(fkName.ToLowerInvariant());
                }

                foreach (var index in entity.GetIndexes())
                {
                    var indexName = index.GetDatabaseName();
                    if (!string.IsNullOrWhiteSpace(indexName))
                        index.SetDatabaseName(indexName.ToLowerInvariant());
                }
            }
        }
    }
}
