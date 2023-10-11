using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class Initiale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes");

            migrationBuilder.AddColumn<decimal>(
                name: "Square",
                table: "iPs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "IPSId",
                table: "Cargoes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Cargoes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.DropColumn(
                name: "Square",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "Weight",
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
