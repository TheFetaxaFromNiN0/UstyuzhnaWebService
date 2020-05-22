using Microsoft.EntityFrameworkCore.Migrations;

namespace Ust.Api.Migrations
{
    public partial class AddCompanyLogoTablEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CompanyLogoId",
                table: "Organizations",
                column: "CompanyLogoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations",
                column: "CompanyLogoId",
                principalTable: "CompanyLogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CompanyLogoId",
                table: "Organizations",
                column: "CompanyLogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations",
                column: "CompanyLogoId",
                principalTable: "CompanyLogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
