using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Natjecaji",
                columns: table => new
                {
                    IDNatjecaj = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcijenjenaVrijednost = table.Column<int>(type: "int", nullable: false),
                    TrajanjeOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrajanjeDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aktivan = table.Column<bool>(type: "bit", nullable: false),
                    Dobitnik = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natjecaji", x => x.IDNatjecaj);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Natjecaji");
        }
    }
}
