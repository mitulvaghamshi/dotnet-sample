using BabyNames.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyNames.Data;

public partial class BabyContext : DbContext
{
    public BabyContext(DbContextOptions<BabyContext> options) : base(options) { }
    public virtual DbSet<Zodiac> Zodiacs { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<Nakshatra> Nakshatras { get; set; }

    public virtual DbSet<Baby> Babies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("name=BabyName");

    protected override void OnModelCreating(ModelBuilder modelBuilder) => OnModelCreatingPartial(modelBuilder);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
