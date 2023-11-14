using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models
{
	public class Genre
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(25)]
		public string? GenreDescription { get; set; }

		// Navigation property
		public List<Movie>? Movies { get; set; } 
	}
}
