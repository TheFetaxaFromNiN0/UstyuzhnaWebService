using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddNewPropertyRussianTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RussianTableName",
                table: "MetaDataInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RussianTableName",
                table: "MetaDataInfo");
        }
    }
}
