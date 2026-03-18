using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseExplorer.Models.ColorPicker;

public partial class Group
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string Name { get; set; } = string.Empty;

    [InverseProperty(nameof(Group))]
    public virtual ICollection<Color> Colors { get; set; } = new List<Color>();
}
