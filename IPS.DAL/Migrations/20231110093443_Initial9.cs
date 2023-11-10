using Microsoft.EntityFrameworkCore.Migrations;

namespace IPS.DAL.Migrations
{
    public partial class Initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Square",
                table: "iPs",
                type: "decimal(18,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "AlfaCoefficient",
                table: "iPs",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BettaCoefficient",
                table: "iPs",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Density",
                table: "iPs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LowLimit",
                table: "iPs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxLimit",
                table: "iPs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "iPs",
                type: "decimal(18,7)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Cargoes",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "Density",
                table: "Cargoes",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Cargoes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlfaCoefficient",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "BettaCoefficient",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "Density",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "LowLimit",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "MaxLimit",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "iPs");

            migrationBuilder.DropColumn(
                name: "Density",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cargoes");

            migrationBuilder.AlterColumn<decimal>(
                name: "Square",
                table: "iPs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,7)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Cargoes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");
        }
    }
}
