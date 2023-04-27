using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyNames.Models;

public partial class Religion
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(10)]
    public string Name { get; set; } = string.Empty;

    [InverseProperty(nameof(Religion))]
    public virtual ICollection<BabyName> BabyNames { get; set; } = new List<BabyName>();
}
