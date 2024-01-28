using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class Initial10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Density",
                table: "Cargoes",
                type: "decimal(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AddColumn<decimal>(
                name: "NominalWeight",
                table: "Cargoes",
                type: "decimal(8,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OrderNumerical",
                table: "Cargoes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NominalWeight",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "OrderNumerical",
                table: "Cargoes");

            migrationBuilder.AlterColumn<decimal>(
                name: "Density",
                table: "Cargoes",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,1)");
        }
    }
}
