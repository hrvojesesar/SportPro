using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Narudzbe_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Narudzbe",
            columns: table => new
            {
                IDNarudzba = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                NazivArtikla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DatumNabave = table.Column<DateTime>(type: "datetime2", nullable: false),
                DatumIsporuke = table.Column<DateTime>(type: "datetime2", nullable: false),
                Kolicina = table.Column<int>(type: "int", nullable: false),
                CijenaPoKomadu = table.Column<double>(type: "float", nullable: false),
                CijenaDostave = table.Column<double>(type: "float", nullable: false),
                Porez = table.Column<double>(type: "float", nullable: true),
                DodatneNaknade = table.Column<double>(type: "float", nullable: true),
                UkupniTrosak = table.Column<double>(type: "float", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Napomene = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DobavljacIDDobavljac = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Narudzbe", x => x.IDNarudzba);
                table.ForeignKey(
                    name: "FK_Narudzbe_Dobavljaci_DobavljacIDDobavljac",
                    column: x => x.DobavljacIDDobavljac,
                    principalTable: "Dobavljaci",
                    principalColumn: "IDDobavljac",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Narudzbe_DobavljacIDDobavljac",
            table: "Narudzbe",
            column: "DobavljacIDDobavljac");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Narudzbe");
    }
}
