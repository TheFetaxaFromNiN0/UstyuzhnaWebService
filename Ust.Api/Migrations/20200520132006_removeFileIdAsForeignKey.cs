using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class removeFileIdAsForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumPhoto_Files_FileId",
                table: "AlbumPhoto");

            migrationBuilder.DropIndex(
                name: "IX_AlbumPhoto_FileId",
                table: "AlbumPhoto");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "AlbumPhoto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "AlbumPhoto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPhoto_FileId",
                table: "AlbumPhoto",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumPhoto_Files_FileId",
                table: "AlbumPhoto",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
