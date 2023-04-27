using ColorPicker.Models;
using Microsoft.EntityFrameworkCore;

namespace ColorPicker.Data;

public partial class ColorContext : DbContext
{
    public ColorContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Color> Color { get; set; }

    public virtual DbSet<Group> Group { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("name=Color");

    protected override void OnModelCreating(ModelBuilder modelBuilder) => OnModelCreatingPartial(modelBuilder);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
