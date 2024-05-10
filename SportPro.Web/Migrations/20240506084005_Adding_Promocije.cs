using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Promocije : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TipoviPromocija",
            columns: table => new
            {
                IDTipPromocije = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TipoviPromocija", x => x.IDTipPromocije);
            });

        migrationBuilder.CreateTable(
            name: "Promocije",
            columns: table => new
            {
                IDPromocije = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                Aktivna = table.Column<bool>(type: "bit", nullable: false),
                DodatniUvjeti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TipoviPromocijaIDTipPromocije = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Promocije", x => x.IDPromocije);
                table.ForeignKey(
                    name: "FK_Promocije_TipoviPromocija_TipoviPromocijaID",
                    column: x => x.TipoviPromocijaIDTipPromocije,
                    principalTable: "TipoviPromocija",
                    principalColumn: "IDTipPromocije",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Promocije_TipoviPromocija",
            table: "Promocije",
            column: "TipoviPromocijaIDTipPromocije");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Promocije");

        migrationBuilder.DropTable(
            name: "TipoviPromocija");
    }
}
