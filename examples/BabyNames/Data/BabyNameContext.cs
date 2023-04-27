using BabyNames.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyNames.Data;

public partial class BabyNameContext : DbContext
{
    public BabyNameContext(DbContextOptions<BabyNameContext> options) : base(options) { }

    public virtual DbSet<Religion> Religion { get; set; }

    public virtual DbSet<BabyName> BabyName { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("name=BabyName");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
