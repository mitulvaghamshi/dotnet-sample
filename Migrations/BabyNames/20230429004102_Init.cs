using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseExplorer.Migrations.BabyNames;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Nakshatras",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Nakshatras", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Religions",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Religions", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Zodiacs",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Latters = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Zodiacs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Babies",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                Meaning = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                Numerology = table.Column<int>(type: "INTEGER", nullable: true),
                Gender = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                Syllables = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                NakshatraId = table.Column<int>(type: "INTEGER", nullable: true),
                ReligionId = table.Column<int>(type: "INTEGER", nullable: true),
                ZodiacId = table.Column<int>(type: "INTEGER", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Babies", x => x.Id);
                table.ForeignKey(
                    name: "FK_Babies_Nakshatras_NakshatraId",
                    column: x => x.NakshatraId,
                    principalTable: "Nakshatras",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Babies_Religions_ReligionId",
                    column: x => x.ReligionId,
                    principalTable: "Religions",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Babies_Zodiacs_ZodiacId",
                    column: x => x.ZodiacId,
                    principalTable: "Zodiacs",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Baby_NakshatraId",
            table: "Babies",
            column: "NakshatraId");

        migrationBuilder.CreateIndex(
            name: "IX_Baby_Name",
            table: "Babies",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_Baby_ReligionId",
            table: "Babies",
            column: "ReligionId");

        migrationBuilder.CreateIndex(
            name: "IX_Baby_ZodiacId",
            table: "Babies",
            column: "ZodiacId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Babies");

        migrationBuilder.DropTable(
            name: "Nakshatras");

        migrationBuilder.DropTable(
            name: "Religions");

        migrationBuilder.DropTable(
            name: "Zodiacs");
    }
}
