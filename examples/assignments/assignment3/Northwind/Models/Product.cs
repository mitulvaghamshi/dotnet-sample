using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class Product
    {
        public int productId { get; set; }

        [Display(Name = "Name")]
        public string productName { get; set; }

        [Display(Name = "Supplier ID")]
        public int supplierId { get; set; }

        public int categoryId { get; set; }

        [Display(Name = "Quantity Per Unit")]
        public string quantityPerUnit { get; set; }
        
        [Display(Name = "Unit Price"), DisplayFormat(DataFormatString = "{0:n2}")]
        public double unitPrice { get; set; }
 
        [Display(Name = "In Stock")]
        public int unitsInStock { get; set; }

        [Display(Name = "On Order")]
        public int unitsOnOrder { get; set; }

        [Display(Name = "Reorder Level")]
        public int reorderLevel { get; set; }

        [Display(Name = "Discontinued")]
        public bool discontinued { get; set; }

        public Category category { get; set; }
    }
}
