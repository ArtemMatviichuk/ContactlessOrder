using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class ChangeOnDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositionModification_Modifications_ModificationId",
                table: "OrderPositionModification");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositionModification_Modifications_ModificationId",
                table: "OrderPositionModification",
                column: "ModificationId",
                principalTable: "Modifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions",
                column: "OptionId",
                principalTable: "CateringMenuOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositionModification_Modifications_ModificationId",
                table: "OrderPositionModification");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositionModification_Modifications_ModificationId",
                table: "OrderPositionModification",
                column: "ModificationId",
                principalTable: "Modifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions",
                column: "OptionId",
                principalTable: "CateringMenuOptions",
                principalColumn: "Id");
        }
    }
}
