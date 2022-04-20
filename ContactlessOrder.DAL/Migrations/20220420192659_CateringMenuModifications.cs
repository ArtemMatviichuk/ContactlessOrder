using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class CateringMenuModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CateringMenuModifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true),
                    InheritPrice = table.Column<bool>(type: "bit", nullable: false),
                    CateringId = table.Column<int>(type: "int", nullable: false),
                    MenuModificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateringMenuModifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CateringMenuModifications_Caterings_CateringId",
                        column: x => x.CateringId,
                        principalTable: "Caterings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CateringMenuModifications_MenuModifications_MenuModificationId",
                        column: x => x.MenuModificationId,
                        principalTable: "MenuModifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CateringMenuModifications_CateringId",
                table: "CateringMenuModifications",
                column: "CateringId");

            migrationBuilder.CreateIndex(
                name: "IX_CateringMenuModifications_MenuModificationId",
                table: "CateringMenuModifications",
                column: "MenuModificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CateringMenuModifications");
        }
    }
}
