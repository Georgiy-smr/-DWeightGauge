using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPS2Cargo",
                columns: table => new
                {
                    IPSId = table.Column<int>(nullable: false),
                    CargoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPS2Cargo", x => new { x.IPSId, x.CargoId });
                    table.ForeignKey(
                        name: "FK_IPS2Cargo_Cargoes_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IPS2Cargo_iPs_IPSId",
                        column: x => x.IPSId,
                        principalTable: "iPs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPS2Cargo_CargoId",
                table: "IPS2Cargo",
                column: "CargoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPS2Cargo");
        }
    }
}
