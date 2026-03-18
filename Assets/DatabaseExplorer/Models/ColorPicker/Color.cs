using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseExplorer.Models.ColorPicker;

public partial class Color
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(10)]
    public string Value { get; set; } = string.Empty;

    [Required]
    public int GroupId { get; set; }

    [ForeignKey(nameof(GroupId))]
    [InverseProperty("Colors")]
    public virtual Group? Group { get; set; }
}
