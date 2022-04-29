using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    public partial class UpdatePayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CateringPaymentMethods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CateringPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CateringId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateringPaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CateringPaymentMethods_Caterings_CateringId",
                        column: x => x.CateringId,
                        principalTable: "Caterings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CateringPaymentMethods_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CateringPaymentMethods_CateringId",
                table: "CateringPaymentMethods",
                column: "CateringId");

            migrationBuilder.CreateIndex(
                name: "IX_CateringPaymentMethods_PaymentMethodId",
                table: "CateringPaymentMethods",
                column: "PaymentMethodId");
        }
    }
}
