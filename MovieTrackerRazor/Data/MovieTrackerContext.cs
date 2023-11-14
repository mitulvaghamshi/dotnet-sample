using Microsoft.EntityFrameworkCore;
using MovieTrackerRazor.Models;

namespace MovieTrackerRazor.Data;

public class MovieTrackerContext : DbContext
{
    public MovieTrackerContext(DbContextOptions<MovieTrackerContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Movie> Movie { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Avengers - End Game", DateSeen = DateTime.Now.AddDays(-7), Genre = "Sci-Fi", Rating = 10, ImageFile = "avengers-end-game.jpg" },
            new Movie { Id = 2, Title = "The Shawshank Redemption", DateSeen = DateTime.Now.AddDays(-150).Date, Genre = "Drama", Rating = 8, ImageFile = "shawshank.jpg" },
            new Movie { Id = 3, Title = "Men in Black", DateSeen = DateTime.Now.AddDays(-250).Date, Genre = "Action", Rating = 7, ImageFile = "meninblack.jpg" },
            new Movie { Id = 4, Title = "The Dark Knight", DateSeen = DateTime.Now.AddDays(-350).Date, Genre = "Action", Rating = 9, ImageFile = "darkknight.jpg" },
            new Movie { Id = 5, Title = "12 Angry Men", DateSeen = DateTime.Now.AddDays(-450).Date, Genre = "Drama", Rating = 7, ImageFile = "12angrymen.jpg" },
            new Movie { Id = 6, Title = "Back to the Future", DateSeen = DateTime.Now.AddDays(-550).Date, Genre = "Adventure", Rating = 8, ImageFile = "backtofuture.jpg" }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("name=MovieTracker");
    }
}
