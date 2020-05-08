using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsModerate",
                table: "Advertisements");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Advertisements",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisements");

            migrationBuilder.AddColumn<bool>(
                name: "IsModerate",
                table: "Advertisements",
                nullable: false,
                defaultValue: false);
        }
    }
}
