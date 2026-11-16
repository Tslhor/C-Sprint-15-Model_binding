using System.Collections.Generic;

namespace ProductsValidation.Models
{
    public class ProductsIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public ProductCategory? Type { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
