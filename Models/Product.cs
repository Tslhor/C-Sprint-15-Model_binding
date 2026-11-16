using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsValidation.Models
{
    public class Product
    {
        public int Id { get; set; }
        public ProductCategory Type { get; set; }

        [Required]
        public string Name { get; set; }

        [DescriptionMatchesName(nameof(Name))]
        public string Description { get; set; }

        [DoubleRange(0, 100000)]
        public decimal Price { get; set; }
    }
}
