using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ust.Api.Migrations
{
    public partial class AddCompanyLogoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyLogoId",
                table: "Organizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CompanyLogos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LogoName = table.Column<string>(nullable: true),
                    DataBytes = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLogos", x => x.Id);
                });

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_CompanyLogos_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "CompanyLogos");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CompanyLogoId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CompanyLogoId",
                table: "Organizations");
        }
    }
}
