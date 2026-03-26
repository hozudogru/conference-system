using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class LowerCaseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PK_sessionıtems",
                table: "sessionıtems");

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
                name: "PK_menuıtems",
                table: "menuıtems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_magiclogintokens",
                table: "magiclogintokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ımportantdates",
                table: "ımportantdates");

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

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "submissions",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SubmissionNumber",
                table: "submissions",
                newName: "submissionnumber");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "submissions",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "RevisionReminderSentAt",
                table: "submissions",
                newName: "revisionremindersentat");

            migrationBuilder.RenameColumn(
                name: "Keywords",
                table: "submissions",
                newName: "keywords");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "submissions",
                newName: "filepath");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "submissions",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "submissions",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "AuthorFullName",
                table: "submissions",
                newName: "authorfullname");

            migrationBuilder.RenameColumn(
                name: "AbstractText",
                table: "submissions",
                newName: "abstracttext");

            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "submissions",
                newName: "ınstitution");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "submissions",
                newName: "conferenceıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "submissions",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_submissions_ConferenceId_Email",
                table: "submissions",
                newName: "ıx_submissions_conferenceıd_email");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "speakers",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "speakers",
                newName: "sortorder");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "speakers",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "speakers",
                newName: "bio");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "speakers",
                newName: "ısactive");

            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "speakers",
                newName: "ınstitution");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "speakers",
                newName: "ımageurl");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "speakers",
                newName: "conferenceıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "speakers",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_speakers_ConferenceId",
                table: "speakers",
                newName: "ıx_speakers_conferenceıd");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "smtpsettings",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "UseSsl",
                table: "smtpsettings",
                newName: "usessl");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "smtpsettings",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "TimeoutMilliseconds",
                table: "smtpsettings",
                newName: "timeoutmilliseconds");

            migrationBuilder.RenameColumn(
                name: "SecurityMode",
                table: "smtpsettings",
                newName: "securitymode");

            migrationBuilder.RenameColumn(
                name: "RequireAuthentication",
                table: "smtpsettings",
                newName: "requireauthentication");

            migrationBuilder.RenameColumn(
                name: "Port",
                table: "smtpsettings",
                newName: "port");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "smtpsettings",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Host",
                table: "smtpsettings",
                newName: "host");

            migrationBuilder.RenameColumn(
                name: "FromName",
                table: "smtpsettings",
                newName: "fromname");

            migrationBuilder.RenameColumn(
                name: "FromEmail",
                table: "smtpsettings",
                newName: "fromemail");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "smtpsettings",
                newName: "ıd");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "sitesettings",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "sitesettings",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sitesettings",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_sitesettings_Key",
                table: "sitesettings",
                newName: "ıx_sitesettings_key");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "sessions",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "sessions",
                newName: "starttime");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "sessions",
                newName: "room");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "sessions",
                newName: "endtime");

            migrationBuilder.RenameColumn(
                name: "ChairName",
                table: "sessions",
                newName: "chairname");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "sessions",
                newName: "conferenceıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sessions",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_sessions_ConferenceId",
                table: "sessions",
                newName: "ıx_sessions_conferenceıd");

            migrationBuilder.RenameColumn(
                name: "PresentationMinutes",
                table: "sessionıtems",
                newName: "presentationminutes");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "sessionıtems",
                newName: "order");

            migrationBuilder.RenameColumn(
                name: "SubmissionId",
                table: "sessionıtems",
                newName: "submissionıd");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "sessionıtems",
                newName: "sessionıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sessionıtems",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_sessionıtems_SubmissionId",
                table: "sessionıtems",
                newName: "ıx_sessionıtems_submissionıd");

            migrationBuilder.RenameIndex(
                name: "IX_sessionıtems_SessionId",
                table: "sessionıtems",
                newName: "ıx_sessionıtems_sessionıd");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "revisionfiles",
                newName: "uploadedat");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "revisionfiles",
                newName: "filepath");

            migrationBuilder.RenameColumn(
                name: "SubmissionId",
                table: "revisionfiles",
                newName: "submissionıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "revisionfiles",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_revisionfiles_SubmissionId",
                table: "revisionfiles",
                newName: "ıx_revisionfiles_submissionıd");

            migrationBuilder.RenameColumn(
                name: "WritingScore",
                table: "reviews",
                newName: "writingscore");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "reviews",
                newName: "submittedat");

            migrationBuilder.RenameColumn(
                name: "RelevanceScore",
                table: "reviews",
                newName: "relevancescore");

            migrationBuilder.RenameColumn(
                name: "Recommendation",
                table: "reviews",
                newName: "recommendation");

            migrationBuilder.RenameColumn(
                name: "OriginalityScore",
                table: "reviews",
                newName: "originalityscore");

            migrationBuilder.RenameColumn(
                name: "MethodScore",
                table: "reviews",
                newName: "methodscore");

            migrationBuilder.RenameColumn(
                name: "CommentToEditor",
                table: "reviews",
                newName: "commenttoeditor");

            migrationBuilder.RenameColumn(
                name: "CommentToAuthor",
                table: "reviews",
                newName: "commenttoauthor");

            migrationBuilder.RenameColumn(
                name: "ReviewAssignmentId",
                table: "reviews",
                newName: "reviewassignmentıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "reviews",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_ReviewAssignmentId",
                table: "reviews",
                newName: "ıx_reviews_reviewassignmentıd");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "reviewers",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "Expertise",
                table: "reviewers",
                newName: "expertise");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "reviewers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "reviewers",
                newName: "ısactive");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "reviewers",
                newName: "ıd");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "reviewassignments",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "ReminderSentAt",
                table: "reviewassignments",
                newName: "remindersentat");

            migrationBuilder.RenameColumn(
                name: "AssignedAt",
                table: "reviewassignments",
                newName: "assignedat");

            migrationBuilder.RenameColumn(
                name: "SubmissionId",
                table: "reviewassignments",
                newName: "submissionıd");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "reviewassignments",
                newName: "reviewerıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "reviewassignments",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_reviewassignments_SubmissionId",
                table: "reviewassignments",
                newName: "ıx_reviewassignments_submissionıd");

            migrationBuilder.RenameIndex(
                name: "IX_reviewassignments_ReviewerId",
                table: "reviewassignments",
                newName: "ıx_reviewassignments_reviewerıd");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "registrationcategories",
                newName: "sortorder");

            migrationBuilder.RenameColumn(
                name: "Fee",
                table: "registrationcategories",
                newName: "fee");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "registrationcategories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "registrationcategories",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "registrationcategories",
                newName: "categoryname");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "registrationcategories",
                newName: "ısactive");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "registrationcategories",
                newName: "conferenceıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "registrationcategories",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_registrationcategories_ConferenceId",
                table: "registrationcategories",
                newName: "ıx_registrationcategories_conferenceıd");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "proceedings",
                newName: "publishedat");

            migrationBuilder.RenameColumn(
                name: "SubmissionId",
                table: "proceedings",
                newName: "submissionıd");

            migrationBuilder.RenameColumn(
                name: "DOI",
                table: "proceedings",
                newName: "doı");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "proceedings",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_proceedings_SubmissionId",
                table: "proceedings",
                newName: "ıx_proceedings_submissionıd");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "pages",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "pages",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "pages",
                newName: "sortorder");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "pages",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "MetaTitle",
                table: "pages",
                newName: "metatitle");

            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "pages",
                newName: "metadescription");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "pages",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "pages",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "IsPublished",
                table: "pages",
                newName: "ıspublished");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pages",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_pages_Slug",
                table: "pages",
                newName: "ıx_pages_slug");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "menuıtems",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "menuıtems",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "menuıtems",
                newName: "sortorder");

            migrationBuilder.RenameColumn(
                name: "MenuLocation",
                table: "menuıtems",
                newName: "menulocation");

            migrationBuilder.RenameColumn(
                name: "MenuGroup",
                table: "menuıtems",
                newName: "menugroup");

            migrationBuilder.RenameColumn(
                name: "OpenInNewTab",
                table: "menuıtems",
                newName: "openınnewtab");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "menuıtems",
                newName: "ısactive");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "menuıtems",
                newName: "ıd");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "magiclogintokens",
                newName: "usertype");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "magiclogintokens",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "magiclogintokens",
                newName: "expiresat");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "magiclogintokens",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "magiclogintokens",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "SubmissionId",
                table: "magiclogintokens",
                newName: "submissionıd");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "magiclogintokens",
                newName: "reviewerıd");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "magiclogintokens",
                newName: "ısused");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "magiclogintokens",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_magiclogintokens_Token",
                table: "magiclogintokens",
                newName: "ıx_magiclogintokens_token");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ımportantdates",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "ımportantdates",
                newName: "sortorder");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ımportantdates",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "DateValue",
                table: "ımportantdates",
                newName: "datevalue");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "ımportantdates",
                newName: "ısactive");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "ımportantdates",
                newName: "conferenceıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ımportantdates",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_ımportantdates_ConferenceId",
                table: "ımportantdates",
                newName: "ıx_ımportantdates_conferenceıd");

            migrationBuilder.RenameColumn(
                name: "ShowSpeakers",
                table: "homepagesettings",
                newName: "showspeakers");

            migrationBuilder.RenameColumn(
                name: "ShowRegistrationCategories",
                table: "homepagesettings",
                newName: "showregistrationcategories");

            migrationBuilder.RenameColumn(
                name: "ShowContact",
                table: "homepagesettings",
                newName: "showcontact");

            migrationBuilder.RenameColumn(
                name: "ShowCommitteeMembers",
                table: "homepagesettings",
                newName: "showcommitteemembers");

            migrationBuilder.RenameColumn(
                name: "ShowAnnouncements",
                table: "homepagesettings",
                newName: "showannouncements");

            migrationBuilder.RenameColumn(
                name: "HighlightBox3Value",
                table: "homepagesettings",
                newName: "highlightbox3value");

            migrationBuilder.RenameColumn(
                name: "HighlightBox3Title",
                table: "homepagesettings",
                newName: "highlightbox3title");

            migrationBuilder.RenameColumn(
                name: "HighlightBox2Value",
                table: "homepagesettings",
                newName: "highlightbox2value");

            migrationBuilder.RenameColumn(
                name: "HighlightBox2Title",
                table: "homepagesettings",
                newName: "highlightbox2title");

            migrationBuilder.RenameColumn(
                name: "HighlightBox1Value",
                table: "homepagesettings",
                newName: "highlightbox1value");

            migrationBuilder.RenameColumn(
                name: "HighlightBox1Title",
                table: "homepagesettings",
                newName: "highlightbox1title");

            migrationBuilder.RenameColumn(
                name: "HeroTitle",
                table: "homepagesettings",
                newName: "herotitle");

            migrationBuilder.RenameColumn(
                name: "HeroSecondaryButtonUrl",
                table: "homepagesettings",
                newName: "herosecondarybuttonurl");

            migrationBuilder.RenameColumn(
                name: "HeroSecondaryButtonText",
                table: "homepagesettings",
                newName: "herosecondarybuttontext");

            migrationBuilder.RenameColumn(
                name: "HeroPrimaryButtonUrl",
                table: "homepagesettings",
                newName: "heroprimarybuttonurl");

            migrationBuilder.RenameColumn(
                name: "HeroPrimaryButtonText",
                table: "homepagesettings",
                newName: "heroprimarybuttontext");

            migrationBuilder.RenameColumn(
                name: "HeroDescription",
                table: "homepagesettings",
                newName: "herodescription");

            migrationBuilder.RenameColumn(
                name: "HeroBadgeText",
                table: "homepagesettings",
                newName: "herobadgetext");

            migrationBuilder.RenameColumn(
                name: "ContactTitle",
                table: "homepagesettings",
                newName: "contacttitle");

            migrationBuilder.RenameColumn(
                name: "ContactHtml",
                table: "homepagesettings",
                newName: "contacthtml");

            migrationBuilder.RenameColumn(
                name: "ShowImportantDates",
                table: "homepagesettings",
                newName: "showımportantdates");

            migrationBuilder.RenameColumn(
                name: "PosterImageUrl",
                table: "homepagesettings",
                newName: "posterımageurl");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "homepagesettings",
                newName: "conferenceıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "homepagesettings",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_homepagesettings_ConferenceId",
                table: "homepagesettings",
                newName: "ıx_homepagesettings_conferenceıd");

            migrationBuilder.RenameColumn(
                name: "DecisionType",
                table: "editorialdecisions",
                newName: "decisiontype");

            migrationBuilder.RenameColumn(
                name: "DecisionNote",
                table: "editorialdecisions",
                newName: "decisionnote");

            migrationBuilder.RenameColumn(
                name: "DecidedAt",
                table: "editorialdecisions",
                newName: "decidedat");

            migrationBuilder.RenameColumn(
                name: "SubmissionId",
                table: "editorialdecisions",
                newName: "submissionıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "editorialdecisions",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_editorialdecisions_SubmissionId",
                table: "editorialdecisions",
                newName: "ıx_editorialdecisions_submissionıd");

            migrationBuilder.RenameColumn(
                name: "WebsiteUrl",
                table: "conferences",
                newName: "websiteurl");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "conferences",
                newName: "startdate");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "conferences",
                newName: "shortname");

            migrationBuilder.RenameColumn(
                name: "PresidentName",
                table: "conferences",
                newName: "presidentname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "conferences",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LogoPath",
                table: "conferences",
                newName: "logopath");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "conferences",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "conferences",
                newName: "enddate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "conferences",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "conferences",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "OrganizingCommitteeInfo",
                table: "conferences",
                newName: "organizingcommitteeınfo");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "conferences",
                newName: "ısactive");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "conferences",
                newName: "ıd");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "committeemembers",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "committeemembers",
                newName: "sortorder");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "committeemembers",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "CommitteeType",
                table: "committeemembers",
                newName: "committeetype");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "committeemembers",
                newName: "ısactive");

            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "committeemembers",
                newName: "ınstitution");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "committeemembers",
                newName: "conferenceıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "committeemembers",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_committeemembers_ConferenceId",
                table: "committeemembers",
                newName: "ıx_committeemembers_conferenceıd");

            migrationBuilder.RenameColumn(
                name: "PerformedBy",
                table: "auditlogs",
                newName: "performedby");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "auditlogs",
                newName: "entitytype");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "auditlogs",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "auditlogs",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "ActionType",
                table: "auditlogs",
                newName: "actiontype");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "auditlogs",
                newName: "ıpaddress");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "auditlogs",
                newName: "entityıd");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "auditlogs",
                newName: "ıd");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "announcements",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "announcements",
                newName: "summary");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "announcements",
                newName: "publishdate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "announcements",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "announcements",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "IsPublished",
                table: "announcements",
                newName: "ıspublished");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "announcements",
                newName: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_submissions",
                table: "submissions",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_speakers",
                table: "speakers",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_smtpsettings",
                table: "smtpsettings",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sitesettings",
                table: "sitesettings",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sessions",
                table: "sessions",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sessionıtems",
                table: "sessionıtems",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_revisionfiles",
                table: "revisionfiles",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_reviews",
                table: "reviews",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_reviewers",
                table: "reviewers",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_reviewassignments",
                table: "reviewassignments",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_registrationcategories",
                table: "registrationcategories",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_proceedings",
                table: "proceedings",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_pages",
                table: "pages",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menuıtems",
                table: "menuıtems",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_magiclogintokens",
                table: "magiclogintokens",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ımportantdates",
                table: "ımportantdates",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_homepagesettings",
                table: "homepagesettings",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_editorialdecisions",
                table: "editorialdecisions",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_conferences",
                table: "conferences",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_committeemembers",
                table: "committeemembers",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_auditlogs",
                table: "auditlogs",
                column: "ıd");

            migrationBuilder.AddPrimaryKey(
                name: "pk_announcements",
                table: "announcements",
                column: "ıd");

            migrationBuilder.AddForeignKey(
                name: "fk_committeemembers_conferences_conferenceıd",
                table: "committeemembers",
                column: "conferenceıd",
                principalTable: "conferences",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_editorialdecisions_submissions_submissionıd",
                table: "editorialdecisions",
                column: "submissionıd",
                principalTable: "submissions",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_homepagesettings_conferences_conferenceıd",
                table: "homepagesettings",
                column: "conferenceıd",
                principalTable: "conferences",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ımportantdates_conferences_conferenceıd",
                table: "ımportantdates",
                column: "conferenceıd",
                principalTable: "conferences",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_proceedings_submissions_submissionıd",
                table: "proceedings",
                column: "submissionıd",
                principalTable: "submissions",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_registrationcategories_conferences_conferenceıd",
                table: "registrationcategories",
                column: "conferenceıd",
                principalTable: "conferences",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_reviewassignments_reviewers_reviewerıd",
                table: "reviewassignments",
                column: "reviewerıd",
                principalTable: "reviewers",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_reviewassignments_submissions_submissionıd",
                table: "reviewassignments",
                column: "submissionıd",
                principalTable: "submissions",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_reviewassignments_reviewassignmentıd",
                table: "reviews",
                column: "reviewassignmentıd",
                principalTable: "reviewassignments",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_revisionfiles_submissions_submissionıd",
                table: "revisionfiles",
                column: "submissionıd",
                principalTable: "submissions",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sessionıtems_sessions_sessionıd",
                table: "sessionıtems",
                column: "sessionıd",
                principalTable: "sessions",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sessionıtems_submissions_submissionıd",
                table: "sessionıtems",
                column: "submissionıd",
                principalTable: "submissions",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sessions_conferences_conferenceıd",
                table: "sessions",
                column: "conferenceıd",
                principalTable: "conferences",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_speakers_conferences_conferenceıd",
                table: "speakers",
                column: "conferenceıd",
                principalTable: "conferences",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_submissions_conferences_conferenceıd",
                table: "submissions",
                column: "conferenceıd",
                principalTable: "conferences",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_committeemembers_conferences_conferenceıd",
                table: "committeemembers");

            migrationBuilder.DropForeignKey(
                name: "fk_editorialdecisions_submissions_submissionıd",
                table: "editorialdecisions");

            migrationBuilder.DropForeignKey(
                name: "fk_homepagesettings_conferences_conferenceıd",
                table: "homepagesettings");

            migrationBuilder.DropForeignKey(
                name: "fk_ımportantdates_conferences_conferenceıd",
                table: "ımportantdates");

            migrationBuilder.DropForeignKey(
                name: "fk_proceedings_submissions_submissionıd",
                table: "proceedings");

            migrationBuilder.DropForeignKey(
                name: "fk_registrationcategories_conferences_conferenceıd",
                table: "registrationcategories");

            migrationBuilder.DropForeignKey(
                name: "fk_reviewassignments_reviewers_reviewerıd",
                table: "reviewassignments");

            migrationBuilder.DropForeignKey(
                name: "fk_reviewassignments_submissions_submissionıd",
                table: "reviewassignments");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_reviewassignments_reviewassignmentıd",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_revisionfiles_submissions_submissionıd",
                table: "revisionfiles");

            migrationBuilder.DropForeignKey(
                name: "fk_sessionıtems_sessions_sessionıd",
                table: "sessionıtems");

            migrationBuilder.DropForeignKey(
                name: "fk_sessionıtems_submissions_submissionıd",
                table: "sessionıtems");

            migrationBuilder.DropForeignKey(
                name: "fk_sessions_conferences_conferenceıd",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "fk_speakers_conferences_conferenceıd",
                table: "speakers");

            migrationBuilder.DropForeignKey(
                name: "fk_submissions_conferences_conferenceıd",
                table: "submissions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_submissions",
                table: "submissions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_speakers",
                table: "speakers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_smtpsettings",
                table: "smtpsettings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sitesettings",
                table: "sitesettings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sessions",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sessionıtems",
                table: "sessionıtems");

            migrationBuilder.DropPrimaryKey(
                name: "pk_revisionfiles",
                table: "revisionfiles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reviews",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reviewers",
                table: "reviewers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reviewassignments",
                table: "reviewassignments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_registrationcategories",
                table: "registrationcategories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_proceedings",
                table: "proceedings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pages",
                table: "pages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menuıtems",
                table: "menuıtems");

            migrationBuilder.DropPrimaryKey(
                name: "pk_magiclogintokens",
                table: "magiclogintokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ımportantdates",
                table: "ımportantdates");

            migrationBuilder.DropPrimaryKey(
                name: "pk_homepagesettings",
                table: "homepagesettings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_editorialdecisions",
                table: "editorialdecisions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_conferences",
                table: "conferences");

            migrationBuilder.DropPrimaryKey(
                name: "pk_committeemembers",
                table: "committeemembers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_auditlogs",
                table: "auditlogs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_announcements",
                table: "announcements");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "submissions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "submissionnumber",
                table: "submissions",
                newName: "SubmissionNumber");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "submissions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "revisionremindersentat",
                table: "submissions",
                newName: "RevisionReminderSentAt");

            migrationBuilder.RenameColumn(
                name: "keywords",
                table: "submissions",
                newName: "Keywords");

            migrationBuilder.RenameColumn(
                name: "filepath",
                table: "submissions",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "submissions",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "submissions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "authorfullname",
                table: "submissions",
                newName: "AuthorFullName");

            migrationBuilder.RenameColumn(
                name: "abstracttext",
                table: "submissions",
                newName: "AbstractText");

            migrationBuilder.RenameColumn(
                name: "ınstitution",
                table: "submissions",
                newName: "Institution");

            migrationBuilder.RenameColumn(
                name: "conferenceıd",
                table: "submissions",
                newName: "ConferenceId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "submissions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_submissions_conferenceıd_email",
                table: "submissions",
                newName: "IX_submissions_ConferenceId_Email");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "speakers",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "sortorder",
                table: "speakers",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "speakers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "bio",
                table: "speakers",
                newName: "Bio");

            migrationBuilder.RenameColumn(
                name: "ısactive",
                table: "speakers",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ınstitution",
                table: "speakers",
                newName: "Institution");

            migrationBuilder.RenameColumn(
                name: "ımageurl",
                table: "speakers",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "conferenceıd",
                table: "speakers",
                newName: "ConferenceId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "speakers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_speakers_conferenceıd",
                table: "speakers",
                newName: "IX_speakers_ConferenceId");

            migrationBuilder.RenameColumn(
                name: "usessl",
                table: "smtpsettings",
                newName: "UseSsl");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "smtpsettings",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "smtpsettings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "timeoutmilliseconds",
                table: "smtpsettings",
                newName: "TimeoutMilliseconds");

            migrationBuilder.RenameColumn(
                name: "securitymode",
                table: "smtpsettings",
                newName: "SecurityMode");

            migrationBuilder.RenameColumn(
                name: "requireauthentication",
                table: "smtpsettings",
                newName: "RequireAuthentication");

            migrationBuilder.RenameColumn(
                name: "port",
                table: "smtpsettings",
                newName: "Port");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "smtpsettings",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "host",
                table: "smtpsettings",
                newName: "Host");

            migrationBuilder.RenameColumn(
                name: "fromname",
                table: "smtpsettings",
                newName: "FromName");

            migrationBuilder.RenameColumn(
                name: "fromemail",
                table: "smtpsettings",
                newName: "FromEmail");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "smtpsettings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "sitesettings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "sitesettings",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "sitesettings",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_sitesettings_key",
                table: "sitesettings",
                newName: "IX_sitesettings_Key");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "sessions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "starttime",
                table: "sessions",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "room",
                table: "sessions",
                newName: "Room");

            migrationBuilder.RenameColumn(
                name: "endtime",
                table: "sessions",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "chairname",
                table: "sessions",
                newName: "ChairName");

            migrationBuilder.RenameColumn(
                name: "conferenceıd",
                table: "sessions",
                newName: "ConferenceId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "sessions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_sessions_conferenceıd",
                table: "sessions",
                newName: "IX_sessions_ConferenceId");

            migrationBuilder.RenameColumn(
                name: "presentationminutes",
                table: "sessionıtems",
                newName: "PresentationMinutes");

            migrationBuilder.RenameColumn(
                name: "order",
                table: "sessionıtems",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "submissionıd",
                table: "sessionıtems",
                newName: "SubmissionId");

            migrationBuilder.RenameColumn(
                name: "sessionıd",
                table: "sessionıtems",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "sessionıtems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_sessionıtems_submissionıd",
                table: "sessionıtems",
                newName: "IX_sessionıtems_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "ıx_sessionıtems_sessionıd",
                table: "sessionıtems",
                newName: "IX_sessionıtems_SessionId");

            migrationBuilder.RenameColumn(
                name: "uploadedat",
                table: "revisionfiles",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "filepath",
                table: "revisionfiles",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "submissionıd",
                table: "revisionfiles",
                newName: "SubmissionId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "revisionfiles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_revisionfiles_submissionıd",
                table: "revisionfiles",
                newName: "IX_revisionfiles_SubmissionId");

            migrationBuilder.RenameColumn(
                name: "writingscore",
                table: "reviews",
                newName: "WritingScore");

            migrationBuilder.RenameColumn(
                name: "submittedat",
                table: "reviews",
                newName: "SubmittedAt");

            migrationBuilder.RenameColumn(
                name: "relevancescore",
                table: "reviews",
                newName: "RelevanceScore");

            migrationBuilder.RenameColumn(
                name: "recommendation",
                table: "reviews",
                newName: "Recommendation");

            migrationBuilder.RenameColumn(
                name: "originalityscore",
                table: "reviews",
                newName: "OriginalityScore");

            migrationBuilder.RenameColumn(
                name: "methodscore",
                table: "reviews",
                newName: "MethodScore");

            migrationBuilder.RenameColumn(
                name: "commenttoeditor",
                table: "reviews",
                newName: "CommentToEditor");

            migrationBuilder.RenameColumn(
                name: "commenttoauthor",
                table: "reviews",
                newName: "CommentToAuthor");

            migrationBuilder.RenameColumn(
                name: "reviewassignmentıd",
                table: "reviews",
                newName: "ReviewAssignmentId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "reviews",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_reviews_reviewassignmentıd",
                table: "reviews",
                newName: "IX_reviews_ReviewAssignmentId");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "reviewers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "expertise",
                table: "reviewers",
                newName: "Expertise");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "reviewers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ısactive",
                table: "reviewers",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "reviewers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "reviewassignments",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "remindersentat",
                table: "reviewassignments",
                newName: "ReminderSentAt");

            migrationBuilder.RenameColumn(
                name: "assignedat",
                table: "reviewassignments",
                newName: "AssignedAt");

            migrationBuilder.RenameColumn(
                name: "submissionıd",
                table: "reviewassignments",
                newName: "SubmissionId");

            migrationBuilder.RenameColumn(
                name: "reviewerıd",
                table: "reviewassignments",
                newName: "ReviewerId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "reviewassignments",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_reviewassignments_submissionıd",
                table: "reviewassignments",
                newName: "IX_reviewassignments_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "ıx_reviewassignments_reviewerıd",
                table: "reviewassignments",
                newName: "IX_reviewassignments_ReviewerId");

            migrationBuilder.RenameColumn(
                name: "sortorder",
                table: "registrationcategories",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "fee",
                table: "registrationcategories",
                newName: "Fee");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "registrationcategories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "registrationcategories",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "categoryname",
                table: "registrationcategories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "ısactive",
                table: "registrationcategories",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "conferenceıd",
                table: "registrationcategories",
                newName: "ConferenceId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "registrationcategories",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_registrationcategories_conferenceıd",
                table: "registrationcategories",
                newName: "IX_registrationcategories_ConferenceId");

            migrationBuilder.RenameColumn(
                name: "publishedat",
                table: "proceedings",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "submissionıd",
                table: "proceedings",
                newName: "SubmissionId");

            migrationBuilder.RenameColumn(
                name: "doı",
                table: "proceedings",
                newName: "DOI");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "proceedings",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_proceedings_submissionıd",
                table: "proceedings",
                newName: "IX_proceedings_SubmissionId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "pages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "pages",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "sortorder",
                table: "pages",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "pages",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "metatitle",
                table: "pages",
                newName: "MetaTitle");

            migrationBuilder.RenameColumn(
                name: "metadescription",
                table: "pages",
                newName: "MetaDescription");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "pages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "pages",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "ıspublished",
                table: "pages",
                newName: "IsPublished");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "pages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_pages_slug",
                table: "pages",
                newName: "IX_pages_Slug");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "menuıtems",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "menuıtems",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "sortorder",
                table: "menuıtems",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "menulocation",
                table: "menuıtems",
                newName: "MenuLocation");

            migrationBuilder.RenameColumn(
                name: "menugroup",
                table: "menuıtems",
                newName: "MenuGroup");

            migrationBuilder.RenameColumn(
                name: "ısactive",
                table: "menuıtems",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "openınnewtab",
                table: "menuıtems",
                newName: "OpenInNewTab");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "menuıtems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usertype",
                table: "magiclogintokens",
                newName: "UserType");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "magiclogintokens",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "expiresat",
                table: "magiclogintokens",
                newName: "ExpiresAt");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "magiclogintokens",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "magiclogintokens",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ısused",
                table: "magiclogintokens",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "submissionıd",
                table: "magiclogintokens",
                newName: "SubmissionId");

            migrationBuilder.RenameColumn(
                name: "reviewerıd",
                table: "magiclogintokens",
                newName: "ReviewerId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "magiclogintokens",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_magiclogintokens_token",
                table: "magiclogintokens",
                newName: "IX_magiclogintokens_Token");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "ımportantdates",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "sortorder",
                table: "ımportantdates",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ımportantdates",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "datevalue",
                table: "ımportantdates",
                newName: "DateValue");

            migrationBuilder.RenameColumn(
                name: "ısactive",
                table: "ımportantdates",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "conferenceıd",
                table: "ımportantdates",
                newName: "ConferenceId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "ımportantdates",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_ımportantdates_conferenceıd",
                table: "ımportantdates",
                newName: "IX_ımportantdates_ConferenceId");

            migrationBuilder.RenameColumn(
                name: "showspeakers",
                table: "homepagesettings",
                newName: "ShowSpeakers");

            migrationBuilder.RenameColumn(
                name: "showregistrationcategories",
                table: "homepagesettings",
                newName: "ShowRegistrationCategories");

            migrationBuilder.RenameColumn(
                name: "showcontact",
                table: "homepagesettings",
                newName: "ShowContact");

            migrationBuilder.RenameColumn(
                name: "showcommitteemembers",
                table: "homepagesettings",
                newName: "ShowCommitteeMembers");

            migrationBuilder.RenameColumn(
                name: "showannouncements",
                table: "homepagesettings",
                newName: "ShowAnnouncements");

            migrationBuilder.RenameColumn(
                name: "highlightbox3value",
                table: "homepagesettings",
                newName: "HighlightBox3Value");

            migrationBuilder.RenameColumn(
                name: "highlightbox3title",
                table: "homepagesettings",
                newName: "HighlightBox3Title");

            migrationBuilder.RenameColumn(
                name: "highlightbox2value",
                table: "homepagesettings",
                newName: "HighlightBox2Value");

            migrationBuilder.RenameColumn(
                name: "highlightbox2title",
                table: "homepagesettings",
                newName: "HighlightBox2Title");

            migrationBuilder.RenameColumn(
                name: "highlightbox1value",
                table: "homepagesettings",
                newName: "HighlightBox1Value");

            migrationBuilder.RenameColumn(
                name: "highlightbox1title",
                table: "homepagesettings",
                newName: "HighlightBox1Title");

            migrationBuilder.RenameColumn(
                name: "herotitle",
                table: "homepagesettings",
                newName: "HeroTitle");

            migrationBuilder.RenameColumn(
                name: "herosecondarybuttonurl",
                table: "homepagesettings",
                newName: "HeroSecondaryButtonUrl");

            migrationBuilder.RenameColumn(
                name: "herosecondarybuttontext",
                table: "homepagesettings",
                newName: "HeroSecondaryButtonText");

            migrationBuilder.RenameColumn(
                name: "heroprimarybuttonurl",
                table: "homepagesettings",
                newName: "HeroPrimaryButtonUrl");

            migrationBuilder.RenameColumn(
                name: "heroprimarybuttontext",
                table: "homepagesettings",
                newName: "HeroPrimaryButtonText");

            migrationBuilder.RenameColumn(
                name: "herodescription",
                table: "homepagesettings",
                newName: "HeroDescription");

            migrationBuilder.RenameColumn(
                name: "herobadgetext",
                table: "homepagesettings",
                newName: "HeroBadgeText");

            migrationBuilder.RenameColumn(
                name: "contacttitle",
                table: "homepagesettings",
                newName: "ContactTitle");

            migrationBuilder.RenameColumn(
                name: "contacthtml",
                table: "homepagesettings",
                newName: "ContactHtml");

            migrationBuilder.RenameColumn(
                name: "showımportantdates",
                table: "homepagesettings",
                newName: "ShowImportantDates");

            migrationBuilder.RenameColumn(
                name: "posterımageurl",
                table: "homepagesettings",
                newName: "PosterImageUrl");

            migrationBuilder.RenameColumn(
                name: "conferenceıd",
                table: "homepagesettings",
                newName: "ConferenceId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "homepagesettings",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_homepagesettings_conferenceıd",
                table: "homepagesettings",
                newName: "IX_homepagesettings_ConferenceId");

            migrationBuilder.RenameColumn(
                name: "decisiontype",
                table: "editorialdecisions",
                newName: "DecisionType");

            migrationBuilder.RenameColumn(
                name: "decisionnote",
                table: "editorialdecisions",
                newName: "DecisionNote");

            migrationBuilder.RenameColumn(
                name: "decidedat",
                table: "editorialdecisions",
                newName: "DecidedAt");

            migrationBuilder.RenameColumn(
                name: "submissionıd",
                table: "editorialdecisions",
                newName: "SubmissionId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "editorialdecisions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_editorialdecisions_submissionıd",
                table: "editorialdecisions",
                newName: "IX_editorialdecisions_SubmissionId");

            migrationBuilder.RenameColumn(
                name: "websiteurl",
                table: "conferences",
                newName: "WebsiteUrl");

            migrationBuilder.RenameColumn(
                name: "startdate",
                table: "conferences",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "shortname",
                table: "conferences",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "presidentname",
                table: "conferences",
                newName: "PresidentName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "conferences",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "logopath",
                table: "conferences",
                newName: "LogoPath");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "conferences",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "enddate",
                table: "conferences",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "conferences",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "conferences",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ısactive",
                table: "conferences",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "organizingcommitteeınfo",
                table: "conferences",
                newName: "OrganizingCommitteeInfo");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "conferences",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "committeemembers",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "sortorder",
                table: "committeemembers",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "committeemembers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "committeetype",
                table: "committeemembers",
                newName: "CommitteeType");

            migrationBuilder.RenameColumn(
                name: "ısactive",
                table: "committeemembers",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ınstitution",
                table: "committeemembers",
                newName: "Institution");

            migrationBuilder.RenameColumn(
                name: "conferenceıd",
                table: "committeemembers",
                newName: "ConferenceId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "committeemembers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ıx_committeemembers_conferenceıd",
                table: "committeemembers",
                newName: "IX_committeemembers_ConferenceId");

            migrationBuilder.RenameColumn(
                name: "performedby",
                table: "auditlogs",
                newName: "PerformedBy");

            migrationBuilder.RenameColumn(
                name: "entitytype",
                table: "auditlogs",
                newName: "EntityType");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "auditlogs",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "auditlogs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "actiontype",
                table: "auditlogs",
                newName: "ActionType");

            migrationBuilder.RenameColumn(
                name: "ıpaddress",
                table: "auditlogs",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "entityıd",
                table: "auditlogs",
                newName: "EntityId");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "auditlogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "announcements",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "summary",
                table: "announcements",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "publishdate",
                table: "announcements",
                newName: "PublishDate");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "announcements",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "announcements",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "ıspublished",
                table: "announcements",
                newName: "IsPublished");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "announcements",
                newName: "Id");

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
                name: "PK_sessionıtems",
                table: "sessionıtems",
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
                name: "PK_menuıtems",
                table: "menuıtems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_magiclogintokens",
                table: "magiclogintokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ımportantdates",
                table: "ımportantdates",
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
    }
}
