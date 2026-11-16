using System.Collections.Generic;
using ProductsValidation.Models;

namespace ProductsValidation.Services
{
    public interface IProductService
    {
        IReadOnlyList<Product> GetProducts(ProductCategory? type = null);
        Product GetProduct(int id);
        int GetNextProductId();
        void AddProduct(Product product);
        bool UpdateProduct(Product product);
        void DeleteProduct(int id);
        List<BulkPriceEditItem> GetBulkPriceEditItems(ProductCategory type);
        void UpdateProductPrices(ProductCategory type, IEnumerable<BulkPriceEditItem> products);
    }
}
