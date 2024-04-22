using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_PonudePoslova_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PonudePoslova",
            columns: table => new
            {
                IDPonudaPoslova = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(200)", nullable: false),
                OpisPosla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BrojOsoba = table.Column<int>(type: "int", nullable: false),
                BrojSati = table.Column<double>(type: "float", nullable: false),
                CijenaSata = table.Column<double>(type: "float", nullable: false),
                PotrebnaOprema = table.Column<string>(type: "nvarchar(200)", nullable: true),
                PocetakRadova = table.Column<DateTime>(type: "datetime2", nullable: false),
                KrajRadova = table.Column<DateTime>(type: "datetime2", nullable: false),
                Lokacija = table.Column<string>(type: "nvarchar(100)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PonudePoslova", x => x.IDPonudaPoslova);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PonudePoslova");
    }
}
