using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Zaposlenici_Poslovi_Raspored : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Raspored",
            columns: table => new
            {
                IDRaspored = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Zaposlenik = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Posao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PocetakRada = table.Column<DateTime>(type: "datetime2", nullable: false),
                KrajRada = table.Column<DateTime>(type: "datetime2", nullable: false),
                BrojSati = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Raspored", x => x.IDRaspored);
            });

        migrationBuilder.CreateTable(
            name: "Poslovi",
            columns: table => new
            {
                IDPosla = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(200)", nullable: false),
                OpisPosla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BrojOsoba = table.Column<int>(type: "int", nullable: false),
                BrojSati = table.Column<double>(type: "float", nullable: false),
                CijenaSata = table.Column<double>(type: "float", nullable: false),
                PotrebnaOprema = table.Column<string>(type: "nvarchar(200)", nullable: true),
                PocetakRadova = table.Column<DateTime>(type: "datetime2", nullable: false),
                KrajRadova = table.Column<DateTime>(type: "datetime2", nullable: false),
                Trosak = table.Column<double>(type: "float", nullable: false),
                Profit = table.Column<double>(type: "float", nullable: false),
                RasporedID = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Poslovi", x => x.IDPosla);
                table.ForeignKey(
                    name: "FK_Poslovi_Raspored",
                    column: x => x.RasporedID,
                    principalTable: "Raspored",
                    principalColumn: "IDRaspored");
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
                Adresa = table.Column<string>(type: "nvarchar(200)", nullable: false),
                Telefon = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                DatumZaposlenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                Pozicija = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Cerfitikati = table.Column<string>(type: "nvarchar(200)", nullable: true),
                Placa = table.Column<double>(type: "float", nullable: false),
                Status = table.Column<bool>(type: "bit", nullable: false),
                RasporedID = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Zaposlenici", x => x.IDZaposlenik);
                table.ForeignKey(
                    name: "FK_Zaposlenici_Raspored",
                    column: x => x.RasporedID,
                    principalTable: "Raspored",
                    principalColumn: "IDRaspored");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Poslovi_Raspored",
            table: "Poslovi",
            column: "RasporedID");

        migrationBuilder.CreateIndex(
            name: "IX_Zaposlenici_Raspored",
            table: "Zaposlenici",
            column: "RasporedID");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Poslovi");

        migrationBuilder.DropTable(
            name: "Zaposlenici");

        migrationBuilder.DropTable(
            name: "Raspored");
    }
}
