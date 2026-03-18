using Microsoft.EntityFrameworkCore;
using MovieTrackerBlazor.Shared;

namespace MovieTrackerBlazor.Server.Data;

public class MovieTrackerContext : DbContext
{
    public MovieTrackerContext(DbContextOptions<MovieTrackerContext> options) : base(options) { }

    public DbSet<Genre> Genre { get; set; } = default!;

    public DbSet<Movie> Movie { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("name=MovieTrackerBlazor");
        }
    }
}
