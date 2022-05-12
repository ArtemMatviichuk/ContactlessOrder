using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class ComplainWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Complains",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Complains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ModifiedById",
                table: "Complains",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Complains_Users_ModifiedById",
                table: "Complains",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_Users_ModifiedById",
                table: "Complains");

            migrationBuilder.DropIndex(
                name: "IX_Complains_ModifiedById",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Complains");
        }
    }
}
