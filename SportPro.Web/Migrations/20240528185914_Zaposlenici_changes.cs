using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Zaposlenici_changes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<double>(
            name: "Bonus",
            table: "Zaposlenici",
            type: "float",
            nullable: true);

        migrationBuilder.AddColumn<double>(
            name: "Placa",
            table: "Zaposlenici",
            type: "float",
            nullable: false,
            defaultValue: 0.0);

        migrationBuilder.AddColumn<double>(
            name: "Prijevoz",
            table: "Zaposlenici",
            type: "float",
            nullable: false,
            defaultValue: 0.0);

        migrationBuilder.AddColumn<double>(
            name: "TopliObrok",
            table: "Zaposlenici",
            type: "float",
            nullable: false,
            defaultValue: 0.0);

        migrationBuilder.AddColumn<double>(
            name: "UkupnaPlaca",
            table: "Zaposlenici",
            type: "float",
            nullable: false,
            defaultValue: 0.0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Bonus",
            table: "Zaposlenici");

        migrationBuilder.DropColumn(
            name: "Placa",
            table: "Zaposlenici");

        migrationBuilder.DropColumn(
            name: "Prijevoz",
            table: "Zaposlenici");

        migrationBuilder.DropColumn(
            name: "TopliObrok",
            table: "Zaposlenici");

        migrationBuilder.DropColumn(
            name: "UkupnaPlaca",
            table: "Zaposlenici");
    }
}
