using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class ComplainStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Complains");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Complains",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Complains");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Complains",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
