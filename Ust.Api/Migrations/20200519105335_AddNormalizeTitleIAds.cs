using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddNormalizeTitleIAds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizeTitle",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_NormalizeTitle",
                table: "Advertisements",
                column: "NormalizeTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_Status",
                table: "Advertisements",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Advertisements_NormalizeTitle",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_Status",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "NormalizeTitle",
                table: "Advertisements");
        }
    }
}
