using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTracker.Migrations
{
    /// <inheritdoc />
    public partial class ReleaseYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateSeen", "ReleaseYear" },
                values: new object[] { new DateTime(2023, 2, 10, 17, 44, 36, 84, DateTimeKind.Local).AddTicks(2273), null });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateSeen", "ReleaseYear" },
                values: new object[] { new DateTime(2023, 3, 7, 17, 44, 36, 84, DateTimeKind.Local).AddTicks(2309), null });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleaseYear",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Movie");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateSeen",
                value: new DateTime(2023, 2, 10, 17, 38, 37, 528, DateTimeKind.Local).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateSeen",
                value: new DateTime(2023, 3, 7, 17, 38, 37, 528, DateTimeKind.Local).AddTicks(7390));
        }
    }
}
