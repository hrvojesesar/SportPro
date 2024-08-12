using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations;

/// <inheritdoc />
public partial class Add_Troskovi : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "KategorijeTroskova",
            columns: table => new
            {
                IDKategorijaTroska = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(200)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_KategorijeTroskova", x => x.IDKategorijaTroska);
            });

        migrationBuilder.CreateTable(
            name: "Troskovi",
            columns: table => new
            {
                IDTrosak = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Naziv = table.Column<string>(type: "nvarchar(100)", nullable: false),
                Opis = table.Column<string>(type: "nvarchar(200)", nullable: true),
                Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                Iznos = table.Column<double>(type: "float", nullable: false),
                Mjesec = table.Column<int>(type: "int", nullable: true),
                Godina = table.Column<int>(type: "int", nullable: true),
                KategorijeTroskovaIDKategorijaTroska = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Troskovi", x => x.IDTrosak);
                table.ForeignKey(
                    name: "FK_Troskovi_KategorijeTroskova_KategorijeTroskovaIDKategorijaTroska",
                    column: x => x.KategorijeTroskovaIDKategorijaTroska,
                    principalTable: "KategorijeTroskova",
                    principalColumn: "IDKategorijaTroska",
                    onDelete: ReferentialAction.Cascade);
            });

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

        migrationBuilder.CreateIndex(
            name: "IX_Troskovi_KategorijeTroskovaIDKategorijaTroska",
            table: "Troskovi",
            column: "KategorijeTroskovaIDKategorijaTroska");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Troskovi");

        migrationBuilder.DropTable(
            name: "KategorijeTroskova");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "098294f1-5451-4a33-a567-5922f7c39c4e",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "084febc5-7687-4113-8aa9-95ce16885067", "AQAAAAIAAYagAAAAEA3asfWNNe3yY5V28Zof8bCnOd2KZAHOLpoRSu+OQpSZM5yS8hw2YuYaL1g3LHKBQw==", "8675a79e-bd04-4453-8134-82c02504a07e" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "2818ecc2-5e74-4df0-a503-57ea46cc3f7c",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "152c66c8-c7d8-419f-8a0a-2eb6c785da11", "AQAAAAIAAYagAAAAEM6BRAa8TNyQQ79SBBowGB5M7/VYkxhswDV2DVBTpTk0Ug6lb1PWPoPa3W0167ZBeA==", "51dd1388-5fa9-489e-9a63-f36dee14e273" });
    }
}
