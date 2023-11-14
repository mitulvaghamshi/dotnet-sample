using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseExplorer.Models.RecipeMaker;

public partial class Category
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string Name { get; set; } = string.Empty;

    [InverseProperty("Category")]
    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
