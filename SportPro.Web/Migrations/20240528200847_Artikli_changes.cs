using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Artikli_changes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<double>(
            name: "UkupnaZarada",
            table: "Artikli",
            type: "float",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "UkupnaZarada",
            table: "Artikli");
    }
}
