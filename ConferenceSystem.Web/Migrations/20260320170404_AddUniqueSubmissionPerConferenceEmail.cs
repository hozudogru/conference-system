using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueSubmissionPerConferenceEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ConferenceId_Email",
                table: "Submissions",
                columns: new[] { "ConferenceId", "Email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_ConferenceId_Email",
                table: "Submissions");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ConferenceId",
                table: "Submissions",
                column: "ConferenceId");
        }
    }
}
