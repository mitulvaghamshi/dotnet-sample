using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTrackerRazor.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    [Range(0, 10)]
    public int? Rating { get; set; }

    [DataType(DataType.Date), DisplayName("Date Seen")]
    public DateTime? DateSeen { get; set; }

    [DisplayName("Poster Name")]
    public string? ImageFile { get; set; }
}
