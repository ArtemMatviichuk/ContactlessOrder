using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class CateringMenuModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CateringMenuItemModifications_MenuItemModifications_MenuItemModificationId",
                table: "CateringMenuItemModifications");

            migrationBuilder.RenameColumn(
                name: "MenuItemModificationId",
                table: "CateringMenuItemModifications",
                newName: "ModificationId");

            migrationBuilder.RenameIndex(
                name: "IX_CateringMenuItemModifications_MenuItemModificationId",
                table: "CateringMenuItemModifications",
                newName: "IX_CateringMenuItemModifications_ModificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CateringMenuItemModifications_Modifications_ModificationId",
                table: "CateringMenuItemModifications",
                column: "ModificationId",
                principalTable: "Modifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CateringMenuItemModifications_Modifications_ModificationId",
                table: "CateringMenuItemModifications");

            migrationBuilder.RenameColumn(
                name: "ModificationId",
                table: "CateringMenuItemModifications",
                newName: "MenuItemModificationId");

            migrationBuilder.RenameIndex(
                name: "IX_CateringMenuItemModifications_ModificationId",
                table: "CateringMenuItemModifications",
                newName: "IX_CateringMenuItemModifications_MenuItemModificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CateringMenuItemModifications_MenuItemModifications_MenuItemModificationId",
                table: "CateringMenuItemModifications",
                column: "MenuItemModificationId",
                principalTable: "MenuItemModifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
