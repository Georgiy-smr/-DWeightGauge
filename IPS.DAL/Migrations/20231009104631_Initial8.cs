using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes");

            migrationBuilder.DropIndex(
                name: "IX_Cargoes_IPSId",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "IPSId",
                table: "Cargoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IPSId",
                table: "Cargoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_IPSId",
                table: "Cargoes",
                column: "IPSId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes",
                column: "IPSId",
                principalTable: "iPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
