using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseExplorer.Models.BabyNames;

[Table("Religions")]
public partial class Religion
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string Name { get; set; } = string.Empty;

    [InverseProperty("Religion")]
    public virtual ICollection<Baby> Babies { get; set; } = new List<Baby>();
}
