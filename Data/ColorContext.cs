using DatabaseExplorer.Models.ColorPicker;
using Microsoft.EntityFrameworkCore;

namespace DatabaseExplorer.Data;

public partial class ColorContext : DbContext
{
    public ColorContext(DbContextOptions<ColorContext> options) : base(options) { }

    public virtual DbSet<Color> Color { get; set; }

    public virtual DbSet<Group> Group { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("name=ColorPicker");

    protected override void OnModelCreating(ModelBuilder modelBuilder) => OnModelCreatingPartial(modelBuilder);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
