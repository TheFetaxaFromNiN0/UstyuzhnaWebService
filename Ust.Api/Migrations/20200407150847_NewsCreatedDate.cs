using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class NewsCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "News",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "clock_timestamp()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "News",
                nullable: false,
                defaultValueSql: "clock_timestamp()",
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "now()");
        }
    }
}
