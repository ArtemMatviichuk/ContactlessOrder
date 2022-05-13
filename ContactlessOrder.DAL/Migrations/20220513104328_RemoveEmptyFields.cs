using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class RemoveEmptyFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caterings_Users_UserId",
                table: "Caterings");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegisteredDate",
                table: "Caterings");

            migrationBuilder.AddForeignKey(
                name: "FK_Caterings_Users_UserId",
                table: "Caterings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caterings_Users_UserId",
                table: "Caterings");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredDate",
                table: "Caterings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Caterings_Users_UserId",
                table: "Caterings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
