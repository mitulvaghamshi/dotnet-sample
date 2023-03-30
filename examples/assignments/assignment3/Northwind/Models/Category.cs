using System.Collections.Generic;

namespace Northwind.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public List<Product> products { get; set; }
    }
}
