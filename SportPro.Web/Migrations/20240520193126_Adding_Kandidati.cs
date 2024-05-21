using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Kandidati : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Kandidati",
            columns: table => new
            {
                IDKandidat = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Ime = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Prezime = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Adresa = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Grad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Drzava = table.Column<string>(type: "nvarchar(50)", nullable: false),
                PostanskiBroj = table.Column<int>(type: "int", nullable: false),
                Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Telefon = table.Column<string>(type: "nvarchar(20)", nullable: false),
                DatumPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                Napomene = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Kandidati", x => x.IDKandidat);
            });

        migrationBuilder.CreateTable(
            name: "KandidatiNatjecaji",
            columns: table => new
            {
                KandidatiIDKandidat = table.Column<int>(type: "int", nullable: false),
                NatjecajiIDNatjecaj = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_KandidatiNatjecaji", x => new { x.KandidatiIDKandidat, x.NatjecajiIDNatjecaj });
                table.ForeignKey(
                    name: "FK_KandidatiNatjecaji_Kandidati_KandidatiIDKandidat",
                    column: x => x.KandidatiIDKandidat,
                    principalTable: "Kandidati",
                    principalColumn: "IDKandidat",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_KandidatiNatjecaji_Natjecaji_NatjecajiIDNatjecaj",
                    column: x => x.NatjecajiIDNatjecaj,
                    principalTable: "Natjecaji",
                    principalColumn: "IDNatjecaj",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_KandidatiNatjecaji_NatjecajiIDNatjecaj",
            table: "KandidatiNatjecaji",
            column: "NatjecajiIDNatjecaj");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "KandidatiNatjecaji");

        migrationBuilder.DropTable(
            name: "Kandidati");
    }
}
