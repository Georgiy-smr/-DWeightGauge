using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes");

            migrationBuilder.DropForeignKey(
                name: "FK_IPS2Cargo_Cargoes_CargoId",
                table: "IPS2Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_IPS2Cargo_iPs_IPSId",
                table: "IPS2Cargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_iPs",
                table: "iPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargoes",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "IPSId",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "Cargoes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "iPs",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Cargoes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_iPs",
                table: "iPs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargoes",
                table: "Cargoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes",
                column: "IPSId",
                principalTable: "iPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPS2Cargo_Cargoes_CargoId",
                table: "IPS2Cargo",
                column: "CargoId",
                principalTable: "Cargoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IPS2Cargo_iPs_IPSId",
                table: "IPS2Cargo",
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

            migrationBuilder.DropForeignKey(
                name: "FK_IPS2Cargo_Cargoes_CargoId",
                table: "IPS2Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_IPS2Cargo_iPs_IPSId",
                table: "IPS2Cargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_iPs",
                table: "iPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargoes",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Cargoes");

            migrationBuilder.AddColumn<int>(
                name: "IPSId",
                table: "iPs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "Cargoes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_iPs",
                table: "iPs",
                column: "IPSId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargoes",
                table: "Cargoes",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_iPs_IPSId",
                table: "Cargoes",
                column: "IPSId",
                principalTable: "iPs",
                principalColumn: "IPSId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPS2Cargo_Cargoes_CargoId",
                table: "IPS2Cargo",
                column: "CargoId",
                principalTable: "Cargoes",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IPS2Cargo_iPs_IPSId",
                table: "IPS2Cargo",
                column: "IPSId",
                principalTable: "iPs",
                principalColumn: "IPSId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
