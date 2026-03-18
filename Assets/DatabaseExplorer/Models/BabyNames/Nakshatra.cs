using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseExplorer.Models.BabyNames;

[Table("Nakshatras")]
public partial class Nakshatra
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(20)] 
    public string Name { get; set; } = string.Empty;

    [InverseProperty("Nakshatra")]
    public virtual ICollection<Baby> Babies { get; set; } = new List<Baby>();
}
