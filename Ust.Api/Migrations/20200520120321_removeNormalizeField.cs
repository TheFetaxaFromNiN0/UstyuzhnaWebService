using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class removeNormalizeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Advertisements_NormalizeTitle",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "NormalizeTitle",
                table: "Advertisements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
