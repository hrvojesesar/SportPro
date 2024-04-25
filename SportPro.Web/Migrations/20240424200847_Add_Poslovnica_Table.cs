using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Add_Poslovnica_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "PoslovnicaID",
            table: "Zaposlenici",
            type: "int",
            nullable: true,
            defaultValue: 0);

        migrationBuilder.AlterColumn<string>(
            name: "Opis",
            table: "Pozicije",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.CreateTable(
            name: "Poslovnice",
            columns: table => new
            {
                IDPoslovnica = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Adresa = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Grad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Drzava = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Telefon = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                RadnoVrijemeOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                RadnoVrijemeDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<string>(type: "nvarchar(10)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Poslovnice", x => x.IDPoslovnica);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Zaposlenici_Poslovnica",
            table: "Zaposlenici",
            column: "PoslovnicaID");

        migrationBuilder.AddForeignKey(
            name: "FK_Zaposlenici_Poslovnice",
            table: "Zaposlenici",
            column: "PoslovnicaID",
            principalTable: "Poslovnice",
            principalColumn: "IDPoslovnica",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Zaposlenici_Poslovnice",
            table: "Zaposlenici");

        migrationBuilder.DropTable(
            name: "Poslovnice");

        migrationBuilder.DropIndex(
            name: "IX_Zaposlenici_Poslovnica",
            table: "Zaposlenici");

        migrationBuilder.DropColumn(
            name: "PoslovnicaIDPoslovnica",
            table: "Zaposlenici");

        migrationBuilder.AlterColumn<string>(
            name: "Opis",
            table: "Pozicije",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);
    }
}
