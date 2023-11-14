using Microsoft.EntityFrameworkCore;
using MovieTracker.Models;

namespace MovieTracker.Data
{
    public class MovieTrackerContext : DbContext
    {
        public MovieTrackerContext(DbContextOptions<MovieTrackerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Movie> Movie { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Birds of Prey", DateSeen = DateTime.Now.AddDays(-50), Genre = "Action", Rating = 6, },
                new Movie { Id = 2, Title = "Palm Springs", DateSeen = DateTime.Now.AddDays(-25), Rating = 7, },
                new Movie { Id = 3, Title = "Hamilton", Genre = "Drama", }
            );
        }
    }
}
