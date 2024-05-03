using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Dobavljaci : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Dobavljaci",
            columns: table => new
            {
                IDDobavljac = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Adresa = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Grad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Drzava = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Telefon = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                PocetakSuradnje = table.Column<DateTime>(type: "datetime2", nullable: true),
                KrajSuradnje = table.Column<DateTime>(type: "datetime2", nullable: true),
                SuradnjaAktivna = table.Column<string>(type: "nvarchar(2)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Dobavljaci", x => x.IDDobavljac);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Dobavljaci");
    }
}