using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Adding_Velicine_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Velicine",
            columns: table => new
            {
                IDVelicina = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Velicina = table.Column<string>(type: "nvarchar(5)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Velicine", x => x.IDVelicina);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Velicine");
    }
}
