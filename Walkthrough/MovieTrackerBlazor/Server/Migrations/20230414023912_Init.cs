using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTrackerBlazor.Server.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Genre",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GenreDescriptios = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Genre", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Movie",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                DateSeen = table.Column<DateTime>(type: "datetime2", nullable: true),
                GenreId = table.Column<int>(type: "int", nullable: true),
                Rating = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Movie", x => x.Id);
                table.ForeignKey(
                    name: "FK_Movie_Genre_GenreId",
                    column: x => x.GenreId,
                    principalTable: "Genre",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Movie_GenreId",
            table: "Movie",
            column: "GenreId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Movie");

        migrationBuilder.DropTable(
            name: "Genre");
    }
}
