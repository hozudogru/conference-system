using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RevisionReminderSentAt",
                table: "Submissions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderSentAt",
                table: "ReviewAssignments",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevisionReminderSentAt",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "ReminderSentAt",
                table: "ReviewAssignments");
        }
    }
}
