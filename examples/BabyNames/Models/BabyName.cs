using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyNames.Models;

public partial class BabyName
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Meaning { get; set; } = string.Empty;

    [Required]
    public int Numerology { get; set; }

    [Required, StringLength(20)]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public int ReligionId { get; set; }

    [ForeignKey(nameof(ReligionId))]
    [InverseProperty("BabyNames")]
    public virtual Religion? Religion { get; set; }
}
