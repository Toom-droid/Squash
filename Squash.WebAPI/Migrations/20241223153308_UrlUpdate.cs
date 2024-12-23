using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squash.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UrlUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Flag",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "Flag",
                table: "Urls");
        }
    }
}
