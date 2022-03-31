using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessOrder.DAL.Migrations
{
    public partial class CateringMenuOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CateringMenuOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true),
                    InheritPrice = table.Column<bool>(type: "bit", nullable: false),
                    CateringId = table.Column<int>(type: "int", nullable: false),
                    MenuOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateringMenuOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CateringMenuOptions_Caterings_CateringId",
                        column: x => x.CateringId,
                        principalTable: "Caterings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CateringMenuOptions_MenuOptions_MenuOptionId",
                        column: x => x.MenuOptionId,
                        principalTable: "MenuOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CateringMenuOptions_CateringId",
                table: "CateringMenuOptions",
                column: "CateringId");

            migrationBuilder.CreateIndex(
                name: "IX_CateringMenuOptions_MenuOptionId",
                table: "CateringMenuOptions",
                column: "MenuOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CateringMenuOptions");
        }
    }
}
