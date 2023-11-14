using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseExplorer.Migrations.RecipeMaker;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Category",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Category", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Recipe",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                Method = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: false),
                Ingredients = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: false),
                PreparationTime = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                CookingTime = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                ReadyIn = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Image = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Recipe", x => x.Id);
                table.ForeignKey(
                    name: "FK_Recipe_Category_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Category",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Recipe_CategoryId",
            table: "Recipe",
            column: "CategoryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Recipe");

        migrationBuilder.DropTable(
            name: "Category");
    }
}
