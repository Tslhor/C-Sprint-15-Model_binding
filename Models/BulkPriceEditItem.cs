using System.ComponentModel.DataAnnotations;

namespace ProductsValidation.Models
{
    public class BulkPriceEditItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DoubleRange(0, 100000)]
        public decimal Price { get; set; }
    }
}
