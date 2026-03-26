using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeMembers_Conferences_ConferenceId",
                table: "CommitteeMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EditorialDecisions_Submissions_SubmissionId",
                table: "EditorialDecisions");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePageSettings_Conferences_ConferenceId",
                table: "HomePageSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportantDates_Conferences_ConferenceId",
                table: "ImportantDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Proceedings_Submissions_SubmissionId",
                table: "Proceedings");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationCategories_Conferences_ConferenceId",
                table: "RegistrationCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewAssignments_Reviewers_ReviewerId",
                table: "ReviewAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewAssignments_Submissions_SubmissionId",
                table: "ReviewAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewAssignments_ReviewAssignmentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RevisionFiles_Submissions_SubmissionId",
                table: "RevisionFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionItems_Sessions_SessionId",
                table: "SessionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionItems_Submissions_SubmissionId",
                table: "SessionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Conferences_ConferenceId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Conferences_ConferenceId",
                table: "Speakers");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Conferences_ConferenceId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speakers",
                table: "Speakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmtpSettings",
                table: "SmtpSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteSettings",
                table: "SiteSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RevisionFiles",
                table: "RevisionFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviewers",
                table: "Reviewers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewAssignments",
                table: "ReviewAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationCategories",
                table: "RegistrationCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proceedings",
                table: "Proceedings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MagicLoginTokens",
                table: "MagicLoginTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomePageSettings",
                table: "HomePageSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EditorialDecisions",
                table: "EditorialDecisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conferences",
                table: "Conferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommitteeMembers",
                table: "CommitteeMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Announcements",
                table: "Announcements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionItems",
                table: "SessionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImportantDates",
                table: "ImportantDates");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "submissions");

            migrationBuilder.RenameTable(
                name: "Speakers",
                newName: "speakers");

            migrationBuilder.RenameTable(
                name: "SmtpSettings",
                newName: "smtpsettings");

            migrationBuilder.RenameTable(
                name: "SiteSettings",
                newName: "sitesettings");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "RevisionFiles",
                newName: "revisionfiles");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "reviews");

            migrationBuilder.RenameTable(
                name: "Reviewers",
                newName: "reviewers");

            migrationBuilder.RenameTable(
                name: "ReviewAssignments",
                newName: "reviewassignments");

            migrationBuilder.RenameTable(
                name: "RegistrationCategories",
                newName: "registrationcategories");

            migrationBuilder.RenameTable(
                name: "Proceedings",
                newName: "proceedings");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "pages");

            migrationBuilder.RenameTable(
                name: "MagicLoginTokens",
                newName: "magiclogintokens");

            migrationBuilder.RenameTable(
                name: "HomePageSettings",
                newName: "homepagesettings");

            migrationBuilder.RenameTable(
                name: "EditorialDecisions",
                newName: "editorialdecisions");

            migrationBuilder.RenameTable(
                name: "Conferences",
                newName: "conferences");

            migrationBuilder.RenameTable(
                name: "CommitteeMembers",
                newName: "committeemembers");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "auditlogs");

            migrationBuilder.RenameTable(
                name: "Announcements",
                newName: "announcements");

            migrationBuilder.RenameTable(
                name: "SessionItems",
                newName: "sessionıtems");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "menuıtems");

            migrationBuilder.RenameTable(
                name: "ImportantDates",
                newName: "ımportantdates");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_ConferenceId_Email",
                table: "submissions",
                newName: "IX_submissions_ConferenceId_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Speakers_ConferenceId",
                table: "speakers",
                newName: "IX_speakers_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_SiteSettings_Key",
                table: "sitesettings",
                newName: "IX_sitesettings_Key");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_ConferenceId",
                table: "sessions",
                newName: "IX_sessions_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_RevisionFiles_SubmissionId",
                table: "revisionfiles",
                newName: "IX_revisionfiles_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewAssignmentId",
                table: "reviews",
                newName: "IX_reviews_ReviewAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewAssignments_SubmissionId",
                table: "reviewassignments",
                newName: "IX_reviewassignments_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewAssignments_ReviewerId",
                table: "reviewassignments",
                newName: "IX_reviewassignments_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrationCategories_ConferenceId",
                table: "registrationcategories",
                newName: "IX_registrationcategories_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Proceedings_SubmissionId",
                table: "proceedings",
                newName: "IX_proceedings_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_Slug",
                table: "pages",
                newName: "IX_pages_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_MagicLoginTokens_Token",
                table: "magiclogintokens",
                newName: "IX_magiclogintokens_Token");

            migrationBuilder.RenameIndex(
                name: "IX_HomePageSettings_ConferenceId",
                table: "homepagesettings",
                newName: "IX_homepagesettings_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_EditorialDecisions_SubmissionId",
                table: "editorialdecisions",
                newName: "IX_editorialdecisions_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeMembers_ConferenceId",
                table: "committeemembers",
                newName: "IX_committeemembers_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionItems_SubmissionId",
                table: "sessionıtems",
                newName: "IX_sessionıtems_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionItems_SessionId",
                table: "sessionıtems",
                newName: "IX_sessionıtems_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_ImportantDates_ConferenceId",
                table: "ımportantdates",
                newName: "IX_ımportantdates_ConferenceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_submissions",
                table: "submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_speakers",
                table: "speakers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_smtpsettings",
                table: "smtpsettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sitesettings",
                table: "sitesettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                table: "sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_revisionfiles",
                table: "revisionfiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviewers",
                table: "reviewers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviewassignments",
                table: "reviewassignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_registrationcategories",
                table: "registrationcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_proceedings",
                table: "proceedings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pages",
                table: "pages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_magiclogintokens",
                table: "magiclogintokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_homepagesettings",
                table: "homepagesettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_editorialdecisions",
                table: "editorialdecisions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_conferences",
                table: "conferences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_committeemembers",
                table: "committeemembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auditlogs",
                table: "auditlogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_announcements",
                table: "announcements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessionıtems",
                table: "sessionıtems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_menuıtems",
                table: "menuıtems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ımportantdates",
                table: "ımportantdates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_committeemembers_conferences_ConferenceId",
                table: "committeemembers",
                column: "ConferenceId",
                principalTable: "conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_editorialdecisions_submissions_SubmissionId",
                table: "editorialdecisions",
                column: "SubmissionId",
                principalTable: "submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_homepagesettings_conferences_ConferenceId",
                table: "homepagesettings",
                column: "ConferenceId",
                principalTable: "conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ımportantdates_conferences_ConferenceId",
                table: "ımportantdates",
                column: "ConferenceId",
                principalTable: "conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_proceedings_submissions_SubmissionId",
                table: "proceedings",
                column: "SubmissionId",
                principalTable: "submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_registrationcategories_conferences_ConferenceId",
                table: "registrationcategories",
                column: "ConferenceId",
                principalTable: "conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviewassignments_reviewers_ReviewerId",
                table: "reviewassignments",
                column: "ReviewerId",
                principalTable: "reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviewassignments_submissions_SubmissionId",
                table: "reviewassignments",
                column: "SubmissionId",
                principalTable: "submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_reviewassignments_ReviewAssignmentId",
                table: "reviews",
                column: "ReviewAssignmentId",
                principalTable: "reviewassignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_revisionfiles_submissions_SubmissionId",
                table: "revisionfiles",
                column: "SubmissionId",
                principalTable: "submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessionıtems_sessions_SessionId",
                table: "sessionıtems",
                column: "SessionId",
                principalTable: "sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessionıtems_submissions_SubmissionId",
                table: "sessionıtems",
                column: "SubmissionId",
                principalTable: "submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_conferences_ConferenceId",
                table: "sessions",
                column: "ConferenceId",
                principalTable: "conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_speakers_conferences_ConferenceId",
                table: "speakers",
                column: "ConferenceId",
                principalTable: "conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_submissions_conferences_ConferenceId",
                table: "submissions",
                column: "ConferenceId",
                principalTable: "conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_committeemembers_conferences_ConferenceId",
                table: "committeemembers");

            migrationBuilder.DropForeignKey(
                name: "FK_editorialdecisions_submissions_SubmissionId",
                table: "editorialdecisions");

            migrationBuilder.DropForeignKey(
                name: "FK_homepagesettings_conferences_ConferenceId",
                table: "homepagesettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ımportantdates_conferences_ConferenceId",
                table: "ımportantdates");

            migrationBuilder.DropForeignKey(
                name: "FK_proceedings_submissions_SubmissionId",
                table: "proceedings");

            migrationBuilder.DropForeignKey(
                name: "FK_registrationcategories_conferences_ConferenceId",
                table: "registrationcategories");

            migrationBuilder.DropForeignKey(
                name: "FK_reviewassignments_reviewers_ReviewerId",
                table: "reviewassignments");

            migrationBuilder.DropForeignKey(
                name: "FK_reviewassignments_submissions_SubmissionId",
                table: "reviewassignments");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_reviewassignments_ReviewAssignmentId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_revisionfiles_submissions_SubmissionId",
                table: "revisionfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_sessionıtems_sessions_SessionId",
                table: "sessionıtems");

            migrationBuilder.DropForeignKey(
                name: "FK_sessionıtems_submissions_SubmissionId",
                table: "sessionıtems");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_conferences_ConferenceId",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_speakers_conferences_ConferenceId",
                table: "speakers");

            migrationBuilder.DropForeignKey(
                name: "FK_submissions_conferences_ConferenceId",
                table: "submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_submissions",
                table: "submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_speakers",
                table: "speakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_smtpsettings",
                table: "smtpsettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sitesettings",
                table: "sitesettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_revisionfiles",
                table: "revisionfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviewers",
                table: "reviewers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviewassignments",
                table: "reviewassignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_registrationcategories",
                table: "registrationcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_proceedings",
                table: "proceedings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pages",
                table: "pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_magiclogintokens",
                table: "magiclogintokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_homepagesettings",
                table: "homepagesettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_editorialdecisions",
                table: "editorialdecisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_conferences",
                table: "conferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_committeemembers",
                table: "committeemembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auditlogs",
                table: "auditlogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_announcements",
                table: "announcements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessionıtems",
                table: "sessionıtems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_menuıtems",
                table: "menuıtems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ımportantdates",
                table: "ımportantdates");

            migrationBuilder.RenameTable(
                name: "submissions",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "speakers",
                newName: "Speakers");

            migrationBuilder.RenameTable(
                name: "smtpsettings",
                newName: "SmtpSettings");

            migrationBuilder.RenameTable(
                name: "sitesettings",
                newName: "SiteSettings");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "Sessions");

            migrationBuilder.RenameTable(
                name: "revisionfiles",
                newName: "RevisionFiles");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "reviewers",
                newName: "Reviewers");

            migrationBuilder.RenameTable(
                name: "reviewassignments",
                newName: "ReviewAssignments");

            migrationBuilder.RenameTable(
                name: "registrationcategories",
                newName: "RegistrationCategories");

            migrationBuilder.RenameTable(
                name: "proceedings",
                newName: "Proceedings");

            migrationBuilder.RenameTable(
                name: "pages",
                newName: "Pages");

            migrationBuilder.RenameTable(
                name: "magiclogintokens",
                newName: "MagicLoginTokens");

            migrationBuilder.RenameTable(
                name: "homepagesettings",
                newName: "HomePageSettings");

            migrationBuilder.RenameTable(
                name: "editorialdecisions",
                newName: "EditorialDecisions");

            migrationBuilder.RenameTable(
                name: "conferences",
                newName: "Conferences");

            migrationBuilder.RenameTable(
                name: "committeemembers",
                newName: "CommitteeMembers");

            migrationBuilder.RenameTable(
                name: "auditlogs",
                newName: "AuditLogs");

            migrationBuilder.RenameTable(
                name: "announcements",
                newName: "Announcements");

            migrationBuilder.RenameTable(
                name: "sessionıtems",
                newName: "SessionItems");

            migrationBuilder.RenameTable(
                name: "menuıtems",
                newName: "MenuItems");

            migrationBuilder.RenameTable(
                name: "ımportantdates",
                newName: "ImportantDates");

            migrationBuilder.RenameIndex(
                name: "IX_submissions_ConferenceId_Email",
                table: "Submissions",
                newName: "IX_Submissions_ConferenceId_Email");

            migrationBuilder.RenameIndex(
                name: "IX_speakers_ConferenceId",
                table: "Speakers",
                newName: "IX_Speakers_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_sitesettings_Key",
                table: "SiteSettings",
                newName: "IX_SiteSettings_Key");

            migrationBuilder.RenameIndex(
                name: "IX_sessions_ConferenceId",
                table: "Sessions",
                newName: "IX_Sessions_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_revisionfiles_SubmissionId",
                table: "RevisionFiles",
                newName: "IX_RevisionFiles_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_ReviewAssignmentId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_reviewassignments_SubmissionId",
                table: "ReviewAssignments",
                newName: "IX_ReviewAssignments_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_reviewassignments_ReviewerId",
                table: "ReviewAssignments",
                newName: "IX_ReviewAssignments_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_registrationcategories_ConferenceId",
                table: "RegistrationCategories",
                newName: "IX_RegistrationCategories_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_proceedings_SubmissionId",
                table: "Proceedings",
                newName: "IX_Proceedings_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_pages_Slug",
                table: "Pages",
                newName: "IX_Pages_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_magiclogintokens_Token",
                table: "MagicLoginTokens",
                newName: "IX_MagicLoginTokens_Token");

            migrationBuilder.RenameIndex(
                name: "IX_homepagesettings_ConferenceId",
                table: "HomePageSettings",
                newName: "IX_HomePageSettings_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_editorialdecisions_SubmissionId",
                table: "EditorialDecisions",
                newName: "IX_EditorialDecisions_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_committeemembers_ConferenceId",
                table: "CommitteeMembers",
                newName: "IX_CommitteeMembers_ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_sessionıtems_SubmissionId",
                table: "SessionItems",
                newName: "IX_SessionItems_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_sessionıtems_SessionId",
                table: "SessionItems",
                newName: "IX_SessionItems_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_ımportantdates_ConferenceId",
                table: "ImportantDates",
                newName: "IX_ImportantDates_ConferenceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speakers",
                table: "Speakers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmtpSettings",
                table: "SmtpSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteSettings",
                table: "SiteSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RevisionFiles",
                table: "RevisionFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviewers",
                table: "Reviewers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewAssignments",
                table: "ReviewAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationCategories",
                table: "RegistrationCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proceedings",
                table: "Proceedings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MagicLoginTokens",
                table: "MagicLoginTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomePageSettings",
                table: "HomePageSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EditorialDecisions",
                table: "EditorialDecisions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conferences",
                table: "Conferences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommitteeMembers",
                table: "CommitteeMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Announcements",
                table: "Announcements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionItems",
                table: "SessionItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImportantDates",
                table: "ImportantDates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeMembers_Conferences_ConferenceId",
                table: "CommitteeMembers",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EditorialDecisions_Submissions_SubmissionId",
                table: "EditorialDecisions",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomePageSettings_Conferences_ConferenceId",
                table: "HomePageSettings",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportantDates_Conferences_ConferenceId",
                table: "ImportantDates",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proceedings_Submissions_SubmissionId",
                table: "Proceedings",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationCategories_Conferences_ConferenceId",
                table: "RegistrationCategories",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewAssignments_Reviewers_ReviewerId",
                table: "ReviewAssignments",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewAssignments_Submissions_SubmissionId",
                table: "ReviewAssignments",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewAssignments_ReviewAssignmentId",
                table: "Reviews",
                column: "ReviewAssignmentId",
                principalTable: "ReviewAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RevisionFiles_Submissions_SubmissionId",
                table: "RevisionFiles",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionItems_Sessions_SessionId",
                table: "SessionItems",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionItems_Submissions_SubmissionId",
                table: "SessionItems",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Conferences_ConferenceId",
                table: "Sessions",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Conferences_ConferenceId",
                table: "Speakers",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Conferences_ConferenceId",
                table: "Submissions",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
