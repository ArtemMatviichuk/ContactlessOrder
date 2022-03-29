using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessOrder.DAL.Migrations
{
    public partial class CateringLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Caterings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Caterings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Caterings");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Caterings");
        }
    }
}
