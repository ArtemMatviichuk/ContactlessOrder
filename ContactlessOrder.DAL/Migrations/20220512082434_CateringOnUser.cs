using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class CateringOnUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Caterings");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Caterings");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Caterings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Caterings_UserId",
                table: "Caterings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Caterings_Users_UserId",
                table: "Caterings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[Roles] (Name, Value)
VALUES (N'Заклад громадського харчування', 5);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caterings_Users_UserId",
                table: "Caterings");

            migrationBuilder.DropIndex(
                name: "IX_Caterings_UserId",
                table: "Caterings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Caterings");

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
    }
}
