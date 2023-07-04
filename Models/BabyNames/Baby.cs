using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseExplorer.Models.BabyNames;

[Table("Babies")]
[Index("Name", Name = "IX_Baby_Name")]
[Index("ZodiacId", Name = "IX_Baby_ZodiacId")]
[Index("ReligionId", Name = "IX_Baby_ReligionId")]
[Index("NakshatraId", Name = "IX_Baby_NakshatraId")]
public partial class Baby
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Meaning { get; set; }

    public int? Numerology { get; set; }

    [Required, StringLength(10)]
    public string Gender { get; set; } = string.Empty;

    [Required, StringLength(5)]
    public string Syllables { get; set; } = string.Empty;

    [DisplayName("Nakshatra")]
    public int? NakshatraId { get; set; }

    [DisplayName("Religion")]
    public int? ReligionId { get; set; }

    [DisplayName("Zodiac")]
    public int? ZodiacId { get; set; }

    [ForeignKey(nameof(NakshatraId))]
    [InverseProperty("Babies")]
    public virtual Nakshatra? Nakshatra { get; set; }

    [ForeignKey(nameof(ReligionId))]
    [InverseProperty("Babies")]
    public virtual Religion? Religion { get; set; }

    [ForeignKey(nameof(ZodiacId))]
    [InverseProperty("Babies")]
    public virtual Zodiac? Zodiac { get; set; }
}
