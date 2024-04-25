using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportPro.Web.Migrations
{
    /// <inheritdoc />
    public partial class Poslovnice_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "RadnoVrijemeOd",
                table: "Poslovnice",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "RadnoVrijemeDo",
                table: "Poslovnice",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumOtvaranja",
                table: "Poslovnice",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumOtvaranja",
                table: "Poslovnice");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RadnoVrijemeOd",
                table: "Poslovnice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RadnoVrijemeDo",
                table: "Poslovnice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }
    }
}
