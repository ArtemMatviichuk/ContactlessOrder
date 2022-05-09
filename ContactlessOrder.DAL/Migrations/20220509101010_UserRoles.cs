using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[Roles] (Name, Value)
VALUES (N'Адміністратор', 1),
       (N'Служба підтримки', 2),
       (N'Компанія', 3),
       (N'Клієнт', 4);

DECLARE @AdminId INT = (SELECT TOP(1) Id FROM [dbo].[Roles] WHERE Value = 1);
DECLARE @CompanyId INT = (SELECT TOP(1) Id FROM [dbo].[Roles] WHERE Value = 3);
DECLARE @ClientId INT = (SELECT TOP(1) Id FROM [dbo].[Roles] WHERE Value = 4);

UPDATE [dbo].[Users]
SET RoleId = @AdminId
WHERE Id = -1;

UPDATE [dbo].[Users]
SET RoleId = @CompanyId
WHERE Id IN (SELECT UserId FROM [dbo].[Companies]);

UPDATE [dbo].[Users]
SET RoleId = @ClientId
WHERE RoleId = 0;
");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");
        }
    }
}
