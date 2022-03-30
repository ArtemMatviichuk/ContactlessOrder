using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessOrder.DAL.Migrations
{
    public partial class CateringCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Caterings");

            migrationBuilder.AddColumn<int>(
                name: "CoordinatesId",
                table: "Caterings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Services",
                table: "Caterings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<float>(type: "real", nullable: false),
                    Lng = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caterings_CoordinatesId",
                table: "Caterings",
                column: "CoordinatesId",
                unique: true,
                filter: "[CoordinatesId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Caterings_Coordinates_CoordinatesId",
                table: "Caterings",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caterings_Coordinates_CoordinatesId",
                table: "Caterings");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Caterings_CoordinatesId",
                table: "Caterings");

            migrationBuilder.DropColumn(
                name: "CoordinatesId",
                table: "Caterings");

            migrationBuilder.DropColumn(
                name: "Services",
                table: "Caterings");

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Caterings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
