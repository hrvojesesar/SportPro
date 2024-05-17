using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_ProSport_Korisnici : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ProSportKorisnici",
            columns: table => new
            {
                IDKorisnik = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Ime = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Prezime = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Spol = table.Column<string>(type: "nvarchar(1)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Lozinka = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Telefon = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Adresa = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Grad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Drzava = table.Column<string>(type: "nvarchar(50)", nullable: false),
                PostanskiBroj = table.Column<int>(type: "int", nullable: false),
                DatumRodenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                DatumRegistracije = table.Column<DateTime>(type: "datetime2", nullable: false),
                Verificiran = table.Column<bool>(type: "bit", nullable: false),
                Status = table.Column<string>(type: "nvarchar(20)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProSportKorisnici", x => x.IDKorisnik);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ProSportKorisnici");
    }
}
