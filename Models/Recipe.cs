using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Models;

public partial class Recipe
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, DisplayName("Category")]
    public int CategoryId { get; set; }

    [Required, StringLength(3000)]
    public string Method { get; set; } = string.Empty;

    [Required, StringLength(3000)]
    public string Ingredients { get; set; } = string.Empty;

    [Required, StringLength(20), DisplayName("Preparation Time")]
    public string PreparationTime { get; set; } = string.Empty;

    [Required, StringLength(20), DisplayName("Cooking Time")]
    public string CookingTime { get; set; } = string.Empty;

    [Required, StringLength(20), DisplayName("Ready In")]
    public string ReadyIn { get; set; } = string.Empty;

    [Required, StringLength(100), DisplayName("Image Url")]
    public string Image { get; set; } = string.Empty;

    [ForeignKey(nameof(CategoryId))]
    [InverseProperty("Recipes")]
    public virtual Category Category { get; set; } = default!;
}
