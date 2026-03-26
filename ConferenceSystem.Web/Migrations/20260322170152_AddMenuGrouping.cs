using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuGrouping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuGroup",
                table: "MenuItems",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MenuLocation",
                table: "MenuItems",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuGroup",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuLocation",
                table: "MenuItems");
        }
    }
}
