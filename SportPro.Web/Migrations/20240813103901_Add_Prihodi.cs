using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Add_Prihodi : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "KategorijePrihoda",
            columns: table => new
            {
                IDKategorijePrihoda = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(200)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_KategorijePrihoda", x => x.IDKategorijePrihoda);
            });

        migrationBuilder.CreateTable(
            name: "Prihodi",
            columns: table => new
            {
                IDPrihod = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(200)", nullable: true),
                Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                Iznos = table.Column<double>(type: "float", nullable: false),
                Mjesec = table.Column<int>(type: "int", nullable: true),
                Godina = table.Column<int>(type: "int", nullable: true),
                KategorijePrihodaIDKategorijePrihoda = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Prihodi", x => x.IDPrihod);
                table.ForeignKey(
                    name: "FK_Prihodi_KategorijePrihoda_KategorijePrihodaIDKategorijePrihoda",
                    column: x => x.KategorijePrihodaIDKategorijePrihoda,
                    principalTable: "KategorijePrihoda",
                    principalColumn: "IDKategorijePrihoda",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "098294f1-5451-4a33-a567-5922f7c39c4e",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "c2600fe3-9db7-4e00-af94-efc2b8cf896d", "AQAAAAIAAYagAAAAEC3QwCjXUeIALQUfkgi4QRK3zWiCJIPljeD6lEPcUnnCBqMNyQREmgpmUk5n5JNh6Q==", "f1ba9b01-2a4d-4d95-bd44-8635ef909dc4" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "2818ecc2-5e74-4df0-a503-57ea46cc3f7c",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "d7251724-897a-4f43-927e-d125ce4c0dec", "AQAAAAIAAYagAAAAEGU8oqzik1LrFse7UK4XG4WY4J9xwBfro0mRjL5SArgKElFAVmoXY9CAxyawkCxY9g==", "c30212dd-6e44-4599-8953-28384768d341" });

        migrationBuilder.CreateIndex(
            name: "IX_Prihodi_KategorijePrihodaIDKategorijePrihoda",
            table: "Prihodi",
            column: "KategorijePrihodaIDKategorijePrihoda");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Prihodi");

        migrationBuilder.DropTable(
            name: "KategorijePrihoda");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "098294f1-5451-4a33-a567-5922f7c39c4e",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "2c78f171-3755-4e0d-a5d1-39d268bfea44", "AQAAAAIAAYagAAAAEHV4Z28/n0YWIr2pVV8Pnb6tK4dlOZ7DJACrpjNuFNFbeaQUfjK0JbT/EofWMW+TRA==", "59f1aed0-3379-4960-99d8-007d2882c456" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "2818ecc2-5e74-4df0-a503-57ea46cc3f7c",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "c2d2c0d9-d12f-4107-9c4d-befe985d731a", "AQAAAAIAAYagAAAAEHW6Ahs/5rRQ1jitIpaxE159J9RgOk9DJOZRHDDl5SBU6FAIIVZTkZ5O6jDfPmviWA==", "9812248d-831d-4074-83b9-aa22c369e353" });
    }
}
