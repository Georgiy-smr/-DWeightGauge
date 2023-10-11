using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes");

            migrationBuilder.AlterColumn<int>(
                name: "IPSId",
                table: "Cargoes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes",
                column: "IPSId",
                principalTable: "iPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes");

            migrationBuilder.AlterColumn<int>(
                name: "IPSId",
                table: "Cargoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
