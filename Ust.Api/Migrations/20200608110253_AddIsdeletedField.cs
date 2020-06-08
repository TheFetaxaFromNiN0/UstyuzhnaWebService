using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddIsdeletedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ustyuzhane",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Organizations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "News",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Laws",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HistoryArticles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Files",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CommentHistories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Albums",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AlbumPhoto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Afisha",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Advertisements",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ustyuzhane");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "News");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Laws");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HistoryArticles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CommentHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AlbumPhoto");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Afisha");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Advertisements");
        }
    }
}
