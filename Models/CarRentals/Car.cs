using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseExplorer.Models.CarRentals;

public class Car
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string Make { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Model { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Color { get; set; } = string.Empty;

    public int Year { get; set; }

    [DataType(DataType.Date), DisplayName("Purchase Date")]
    public DateTime PurchaseDate { get; set; }

    [Range(0, 999_999), DisplayFormat(DataFormatString = "{0:N0}")]
    public int Kilometers { get; set; }
}
