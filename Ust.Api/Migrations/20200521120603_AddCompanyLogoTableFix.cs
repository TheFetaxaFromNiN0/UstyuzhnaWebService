using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddCompanyLogoTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyLogoId",
                table: "Organizations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations",
                column: "CompanyLogoId",
                principalTable: "CompanyLogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyLogoId",
                table: "Organizations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations",
                column: "CompanyLogoId",
                principalTable: "CompanyLogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
