using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace assignment2.Models
{
    public partial class Product
    {
        public Product() => OrderDetails = new HashSet<OrderDetail>();

        public int ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Price")]
        // [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal? UnitPrice { get; set; }
        
        [Display(Name = "In Stock")]
        public short? UnitsInStock { get; set; }

        [Display(Name = "On Order")]
        public short? UnitsOnOrder { get; set; }

        [Display(Name = "Reorder Level")]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
