using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.Data;

public partial class RecipeContext : DbContext
{
    public RecipeContext(DbContextOptions<RecipeContext> options) : base(options) { }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Recipe> Recipe { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("name=RecipeContext");
}
