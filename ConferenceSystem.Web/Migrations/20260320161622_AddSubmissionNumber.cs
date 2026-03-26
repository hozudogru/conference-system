using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmissionNumber",
                table: "Submissions",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionNumber",
                table: "Submissions");
        }
    }
}
