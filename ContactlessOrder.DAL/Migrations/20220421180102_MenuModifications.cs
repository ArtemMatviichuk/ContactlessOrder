using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class MenuModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifications_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemModifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    ModificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemModifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemModifications_Menu_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemModifications_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CateringMenuItemModifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    CateringId = table.Column<int>(type: "int", nullable: false),
                    MenuItemModificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateringMenuItemModifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CateringMenuItemModifications_Caterings_CateringId",
                        column: x => x.CateringId,
                        principalTable: "Caterings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CateringMenuItemModifications_MenuItemModifications_MenuItemModificationId",
                        column: x => x.MenuItemModificationId,
                        principalTable: "MenuItemModifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CateringMenuItemModifications_CateringId",
                table: "CateringMenuItemModifications",
                column: "CateringId");

            migrationBuilder.CreateIndex(
                name: "IX_CateringMenuItemModifications_MenuItemModificationId",
                table: "CateringMenuItemModifications",
                column: "MenuItemModificationId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemModifications_MenuItemId",
                table: "MenuItemModifications",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemModifications_ModificationId",
                table: "MenuItemModifications",
                column: "ModificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_CompanyId",
                table: "Modifications",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CateringMenuItemModifications");

            migrationBuilder.DropTable(
                name: "MenuItemModifications");

            migrationBuilder.DropTable(
                name: "Modifications");
        }
    }
}
