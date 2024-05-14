using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Artikli_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Artikli",
            columns: table => new
            {
                IDArtikal = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Cijena = table.Column<double>(type: "float", nullable: false),
                Snizen = table.Column<bool>(type: "bit", nullable: false),
                SnizenaCijena = table.Column<double>(type: "float", nullable: false),
                NabavnaKolicina = table.Column<int>(type: "int", nullable: false),
                TrenutnaKolicina = table.Column<int>(type: "int", nullable: false),
                NaStanju = table.Column<bool>(type: "bit", nullable: false),
                DatumNabave = table.Column<DateTime>(type: "datetime2", nullable: false),
                NabavnaCijena = table.Column<double>(type: "float", nullable: false),
                CijenaDostave = table.Column<double>(type: "float", nullable: false),
                UkupniTrosak = table.Column<double>(type: "float", nullable: false),
                DobavljacIDDobavljac = table.Column<int>(type: "int", nullable: false),
                BrendIDBrend = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Artikli", x => x.IDArtikal);
                table.ForeignKey(
                    name: "FK_Artikli_Brendovi_BrendIDBrend",
                    column: x => x.BrendIDBrend,
                    principalTable: "Brendovi",
                    principalColumn: "IDBrend",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Artikli_Dobavljaci_DobavljacIDDobavljac",
                    column: x => x.DobavljacIDDobavljac,
                    principalTable: "Dobavljaci",
                    principalColumn: "IDDobavljac",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ArtikliBoje",
            columns: table => new
            {
                ArtikliIDArtikal = table.Column<int>(type: "int", nullable: false),
                BojeIDBoja = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ArtikliBoje", x => new { x.ArtikliIDArtikal, x.BojeIDBoja });
                table.ForeignKey(
                    name: "FK_ArtikliBoje_Artikli_ArtikliIDArtikal",
                    column: x => x.ArtikliIDArtikal,
                    principalTable: "Artikli",
                    principalColumn: "IDArtikal",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ArtikliBoje_Boje_BojeIDBoja",
                    column: x => x.BojeIDBoja,
                    principalTable: "Boje",
                    principalColumn: "IDBoja",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ArtikliKategorije",
            columns: table => new
            {
                ArtikliIDArtikal = table.Column<int>(type: "int", nullable: false),
                KategorijeIDKategorija = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ArtikliKategorije", x => new { x.ArtikliIDArtikal, x.KategorijeIDKategorija });
                table.ForeignKey(
                    name: "FK_ArtikliKategorije_Artikli_ArtikliIDArtikal",
                    column: x => x.ArtikliIDArtikal,
                    principalTable: "Artikli",
                    principalColumn: "IDArtikal",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ArtikliKategorije_Kategorije_KategorijeIDKategorija",
                    column: x => x.KategorijeIDKategorija,
                    principalTable: "Kategorije",
                    principalColumn: "IDKategorija",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ArtikliPoslovnice",
            columns: table => new
            {
                ArtikliIDArtikal = table.Column<int>(type: "int", nullable: false),
                PoslovniceIDPoslovnica = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ArtikliPoslovnice", x => new { x.ArtikliIDArtikal, x.PoslovniceIDPoslovnica });
                table.ForeignKey(
                    name: "FK_ArtikliPoslovnice_Artikli_ArtikliIDArtikal",
                    column: x => x.ArtikliIDArtikal,
                    principalTable: "Artikli",
                    principalColumn: "IDArtikal",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ArtikliPoslovnice_Poslovnice_PoslovniceIDPoslovnica",
                    column: x => x.PoslovniceIDPoslovnica,
                    principalTable: "Poslovnice",
                    principalColumn: "IDPoslovnica",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ArtikliVelicine",
            columns: table => new
            {
                ArtikliIDArtikal = table.Column<int>(type: "int", nullable: false),
                VelicineIDVelicina = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ArtikliVelicine", x => new { x.ArtikliIDArtikal, x.VelicineIDVelicina });
                table.ForeignKey(
                    name: "FK_ArtikliVelicine_Artikli_ArtikliIDArtikal",
                    column: x => x.ArtikliIDArtikal,
                    principalTable: "Artikli",
                    principalColumn: "IDArtikal",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ArtikliVelicine_Velicine_VelicineIDVelicina",
                    column: x => x.VelicineIDVelicina,
                    principalTable: "Velicine",
                    principalColumn: "IDVelicina",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Artikli_BrendIDBrend",
            table: "Artikli",
            column: "BrendIDBrend");

        migrationBuilder.CreateIndex(
            name: "IX_Artikli_DobavljacIDDobavljac",
            table: "Artikli",
            column: "DobavljacIDDobavljac");

        migrationBuilder.CreateIndex(
            name: "IX_ArtikliBoje_BojeIDBoja",
            table: "ArtikliBoje",
            column: "BojeIDBoja");

        migrationBuilder.CreateIndex(
            name: "IX_ArtikliKategorije_KategorijeIDKategorija",
            table: "ArtikliKategorije",
            column: "KategorijeIDKategorija");

        migrationBuilder.CreateIndex(
            name: "IX_ArtikliPoslovnice_PoslovniceIDPoslovnica",
            table: "ArtikliPoslovnice",
            column: "PoslovniceIDPoslovnica");

        migrationBuilder.CreateIndex(
            name: "IX_ArtikliVelicine_VelicineIDVelicina",
            table: "ArtikliVelicine",
            column: "VelicineIDVelicina");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ArtikliBoje");

        migrationBuilder.DropTable(
            name: "ArtikliKategorije");

        migrationBuilder.DropTable(
            name: "ArtikliPoslovnice");

        migrationBuilder.DropTable(
            name: "ArtikliVelicine");

        migrationBuilder.DropTable(
            name: "Artikli");
    }
}
