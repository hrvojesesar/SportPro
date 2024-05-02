using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Added_Brendovi : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Brendovi",
            columns: table => new
            {
                IDBrend = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                NazivBrenda = table.Column<string>(type: "nvarchar(20)", nullable: false),
                Vrsta = table.Column<string>(type: "nvarchar(10)", nullable: false),
                Osnivac = table.Column<string>(type: "nvarchar(50)", nullable: false),
                GodinaOsnivanja = table.Column<int>(type: "int", nullable: false),
                Sjediste = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Predsjednik = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Status = table.Column<string>(type: "nvarchar(10)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Brendovi", x => x.IDBrend);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Brendovi");
    }
}
