using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Certifikati : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Certifikati",
            columns: table => new
            {
                IDCertifikat = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DatumDodjele = table.Column<DateTime>(type: "datetime2", nullable: false),
                Organizacija = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Certifikati", x => x.IDCertifikat);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Certifikati");
    }
}
