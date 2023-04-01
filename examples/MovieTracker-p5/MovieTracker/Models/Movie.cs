using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTracker.Models
{
	public class Movie
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string? Title { get; set; }

		[DisplayName("Date Seen")]
		[DataType(DataType.Date)]
		[DateSeenValidation(ErrorMessage = "Date Seen cannot be a future date.")]
		public DateTime? DateSeen { get; set; }

		// Navigation property
		[ForeignKey(nameof(Genre))]
		[Display(Name = nameof(Genre))]
		public int? GenreId { get; set; }

		// Navigation property
		public Genre? Genre { get; set; }

		[Range(1, 10)]
		public int? Rating { get; set; }

		[Display(Name = "Release Year")]
		public int? ReleaseYear { get; set; }
	}
}
