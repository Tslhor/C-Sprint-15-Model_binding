using System.Collections.Generic;

namespace ProductsValidation.Models
{
    public class BulkPriceEditViewModel
    {
        public ProductCategory Type { get; set; }

        public List<BulkPriceEditItem> Products { get; set; } = new List<BulkPriceEditItem>();
    }
}
