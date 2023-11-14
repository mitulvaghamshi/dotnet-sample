using Microsoft.EntityFrameworkCore;
using MovieTracker.Models;

namespace MovieTracker.Data
{
	public class MovieTrackerContext : DbContext
	{
		public MovieTrackerContext(DbContextOptions<MovieTrackerContext> options)
		: base(options)
		{
			// Comment out for runnung Tests, which usage InMemory database.
			// Also, when using Migration...
			// Database.EnsureCreated();
		}

		public DbSet<Movie> Movie { get; set; } = default!;

		public DbSet<Genre> Genres { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			int id = 0;
			var genres = new List<string> {
				"Action", "Adventure", "Animation", "Biography", "Comedy",
				"Crime", "Documentary", "Drama", "Family", "Fantasy",
				"Film Noir", "History", "Horror", "Music", "Musical",
				"Mystery", "Romance", "Sci-Fi", "Short Film", "Sport",
				"Superhero", "Thriller", "War", "Western"
			};
			modelBuilder.Entity<Genre>().HasData(
				from genre in genres
				select new Genre { Id = ++id, GenreDescription = genre }
			);

			modelBuilder.Entity<Movie>().HasData(
				new Movie { Id = 1, Title = "Birds of Prey", DateSeen = DateTime.Now.AddDays(-50), GenreId = 1, Rating = 6 },
				new Movie { Id = 2, Title = "Palm Springs", DateSeen = DateTime.Now.AddDays(-25), Rating = 7 },
				new Movie { Id = 3, Title = "Hamilton", GenreId = 8 }
			);
		}
	}
}
