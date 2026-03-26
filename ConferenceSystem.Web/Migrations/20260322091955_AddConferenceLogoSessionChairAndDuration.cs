using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddConferenceLogoSessionChairAndDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChairName",
                table: "Sessions",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PresentationMinutes",
                table: "SessionItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Conferences",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OrganizingCommitteeInfo",
                table: "Conferences",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PresidentName",
                table: "Conferences",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChairName",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "PresentationMinutes",
                table: "SessionItems");

            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "OrganizingCommitteeInfo",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "PresidentName",
                table: "Conferences");
        }
    }
}
