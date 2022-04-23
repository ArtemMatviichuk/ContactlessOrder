using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class OrderUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions");

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "OrderPositions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "InMomentPrice",
                table: "OrderPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OptionName",
                table: "OrderPositions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderPositionModification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InMomentPrice = table.Column<int>(type: "int", nullable: false),
                    ModificationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OrderPositionId = table.Column<int>(type: "int", nullable: false),
                    ModificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPositionModification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPositionModification_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalTable: "Modifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderPositionModification_OrderPositions_OrderPositionId",
                        column: x => x.OrderPositionId,
                        principalTable: "OrderPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPositionModification_ModificationId",
                table: "OrderPositionModification",
                column: "ModificationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPositionModification_OrderPositionId",
                table: "OrderPositionModification",
                column: "OrderPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions",
                column: "OptionId",
                principalTable: "CateringMenuOptions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions");

            migrationBuilder.DropTable(
                name: "OrderPositionModification");

            migrationBuilder.DropColumn(
                name: "InMomentPrice",
                table: "OrderPositions");

            migrationBuilder.DropColumn(
                name: "OptionName",
                table: "OrderPositions");

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "OrderPositions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_CateringMenuOptions_OptionId",
                table: "OrderPositions",
                column: "OptionId",
                principalTable: "CateringMenuOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
