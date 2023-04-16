using System.ComponentModel.DataAnnotations;

namespace MovieTrackerBlazor.Shared;

public class Genre
{
    [Key]
    public int Id { get; set; }

    [StringLength(25)]
    public string? GenreDescriptios { get; set; }
}
