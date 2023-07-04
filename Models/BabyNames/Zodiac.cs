using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseExplorer.Models.BabyNames;

[Table("Zodiacs")]
public partial class Zodiac
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string Latters { get; set; } = string.Empty;

    [InverseProperty("Zodiac")]
    public virtual ICollection<Baby> Babies { get; set; } = new List<Baby>();
}
