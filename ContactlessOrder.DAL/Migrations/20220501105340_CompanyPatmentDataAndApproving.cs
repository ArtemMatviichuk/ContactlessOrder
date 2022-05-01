using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class CompanyPatmentDataAndApproving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Mfo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CurrentAccount = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentData_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ApprovedById",
                table: "Companies",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentData_CompanyId",
                table: "PaymentData",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_ApprovedById",
                table: "Companies",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_ApprovedById",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "PaymentData");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ApprovedById",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Companies");
        }
    }
}
