using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTrackerBlazor.Shared;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string? Title { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateSeen { get; set; }

    [ForeignKey(nameof(Genre))]
    public int? GenreId { get; set; }

    public Genre? Genre { get; set; }

    [Range(1, 10)]
    public int? Rating { get; set; }
}
