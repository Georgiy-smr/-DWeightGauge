using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes");

            migrationBuilder.AlterColumn<int>(
                name: "IPSId",
                table: "Cargoes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes",
                column: "IPSId",
                principalTable: "iPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes",
                column: "IPSId",
                principalTable: "iPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
