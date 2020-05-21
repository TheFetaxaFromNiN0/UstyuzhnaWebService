using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddAlbumidinAlbumPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "AlbumPhoto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPhoto_AlbumId",
                table: "AlbumPhoto",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumPhoto_Albums_AlbumId",
                table: "AlbumPhoto",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumPhoto_Albums_AlbumId",
                table: "AlbumPhoto");

            migrationBuilder.DropIndex(
                name: "IX_AlbumPhoto_AlbumId",
                table: "AlbumPhoto");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "AlbumPhoto");
        }
    }
}
