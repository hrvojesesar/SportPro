using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Add_Zaposlenici_Pozicije_Tables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Pozicije",
            columns: table => new
            {
                IDPozicija = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pozicije", x => x.IDPozicija);
            });

        migrationBuilder.CreateTable(
            name: "Zaposlenici",
            columns: table => new
            {
                IDZaposlenik = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Ime = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Prezime = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Spol = table.Column<string>(type: "nvarchar(1)", nullable: false),
                Adresa = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Grad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Drzava = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Telefon = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(50)", nullable: true),
                DatumZaposlenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                Certifikati = table.Column<string>(type: "nvarchar(max)", nullable: true),
                JMBG = table.Column<string>(type: "nvarchar(20)", nullable: false),
                BrojBankovnogRacuna = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Kvalifikacija = table.Column<string>(type: "nvarchar(4)", nullable: false),
                DatumZavrsetkaRadnogOdnosa = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Zaposlenici", x => x.IDZaposlenik);
            });

        migrationBuilder.CreateTable(
            name: "PozicijeZaposlenici",
            columns: table => new
            {
                PozicijeID = table.Column<int>(type: "int", nullable: false),
                ZaposleniciID = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PozicijeZaposlenici", x => new { x.PozicijeID, x.ZaposleniciID });
                table.ForeignKey(
                    name: "FK_PozicijeZaposlenici_Pozicije",
                    column: x => x.PozicijeID,
                    principalTable: "Pozicije",
                    principalColumn: "IDPozicija",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PozicijeZaposlenici_Zaposlenici",
                    column: x => x.ZaposleniciID,
                    principalTable: "Zaposlenici",
                    principalColumn: "IDZaposlenik",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PozicijeZaposlenici",
            table: "PozicijeZaposlenici",
            column: "ZaposleniciID");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PozicijeZaposlenici");

        migrationBuilder.DropTable(
            name: "Pozicije");

        migrationBuilder.DropTable(
            name: "Zaposlenici");
    }
}
