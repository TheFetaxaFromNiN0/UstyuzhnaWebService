using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddGalleryAndOtherSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albums");

            migrationBuilder.RenameColumn(
                name: "MadeBy",
                table: "Albums",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Albums",
                newName: "TotalPhotoCount");

            migrationBuilder.RenameIndex(
                name: "IX_Album_Name",
                table: "Albums",
                newName: "IX_Albums_Name");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastDownloadDate",
                table: "Albums",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Albums",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RewiewCount",
                table: "Albums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Albums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ViewCount",
                table: "Albums",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MetaDataInfo_TableName",
                table: "MetaDataInfo",
                column: "TableName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ThemeId",
                table: "Albums",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserId",
                table: "Albums",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_UserId",
                table: "Albums",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_UserId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_MetaDataInfo_TableName",
                table: "MetaDataInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ThemeId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_UserId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "LastDownloadDate",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "RewiewCount",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "Album");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Album",
                newName: "MadeBy");

            migrationBuilder.RenameColumn(
                name: "TotalPhotoCount",
                table: "Album",
                newName: "Count");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_Name",
                table: "Album",
                newName: "IX_Album_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");
        }
    }
}
