using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models
{
	public class Movie
	{
		[Key, Display(Name = "Movie ID")]
		public int Id { get; set; }

		[Required]
		public string? Title { get; set; }

		[DisplayName("Date Seen")]
		[DataType(DataType.Date)]
		public DateTime? DateSeen { get; set; }

		public string? Genre { get; set; }

		[Range(1, 10)]
		public int? Rating { get; set; }
	}
}
