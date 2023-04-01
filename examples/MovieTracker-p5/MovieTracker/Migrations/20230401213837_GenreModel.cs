using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieTracker.Migrations
{
    /// <inheritdoc />
    public partial class GenreModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreDescription = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreDescription" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Animation" },
                    { 4, "Biography" },
                    { 5, "Comedy" },
                    { 6, "Crime" },
                    { 7, "Documentary" },
                    { 8, "Drama" },
                    { 9, "Family" },
                    { 10, "Fantasy" },
                    { 11, "Film Noir" },
                    { 12, "History" },
                    { 13, "Horror" },
                    { 14, "Music" },
                    { 15, "Musical" },
                    { 16, "Mystery" },
                    { 17, "Romance" },
                    { 18, "Sci-Fi" },
                    { 19, "Short Film" },
                    { 20, "Sport" },
                    { 21, "Superhero" },
                    { 22, "Thriller" },
                    { 23, "War" },
                    { 24, "Western" }
                });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateSeen", "GenreId" },
                values: new object[] { new DateTime(2023, 2, 10, 17, 38, 37, 528, DateTimeKind.Local).AddTicks(7357), 1 });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateSeen", "GenreId" },
                values: new object[] { new DateTime(2023, 3, 7, 17, 38, 37, 528, DateTimeKind.Local).AddTicks(7390), null });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "GenreId",
                value: 8);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movie",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movie",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateSeen", "Genre" },
                values: new object[] { new DateTime(2023, 2, 10, 16, 58, 37, 418, DateTimeKind.Local).AddTicks(2513), "Action" });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateSeen", "Genre" },
                values: new object[] { new DateTime(2023, 3, 7, 16, 58, 37, 418, DateTimeKind.Local).AddTicks(2547), null });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "Genre",
                value: "Drama");
        }
    }
}
