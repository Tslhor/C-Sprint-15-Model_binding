using System.Collections.Generic;
using System.Linq;
using ProductsValidation.Models;

namespace ProductsValidation.Services
{
    public class ProductService : IProductService
    {
        private readonly object syncRoot = new object();
        private readonly Data data;

        public ProductService(Data data)
        {
            this.data = data;
        }

        public IReadOnlyList<Product> GetProducts(ProductCategory? type = null)
        {
            lock (syncRoot)
            {
                var products = type.HasValue
                    ? data.Products.Where(p => p.Type == type.Value)
                    : data.Products;

                return products.Select(p => p.ToCopy()).ToList();
            }
        }

        public Product GetProduct(int id)
        {
            lock (syncRoot)
            {
                var found = data.Products.FirstOrDefault(p => p.Id == id);
                return found.ToCopy();
            }
        }

        public int GetNextProductId()
        {
            lock (syncRoot)
            {
                return data.Products.Any() ? data.Products.Max(p => p.Id) + 1 : 1;
            }
        }

        public void AddProduct(Product product)
        {
            lock (syncRoot)
            {
                data.Products.Add(product.ToCopy());
            }
        }

        public bool UpdateProduct(Product product)
        {
            lock (syncRoot)
            {
                var index = data.Products.FindIndex(p => p.Id == product.Id);
                if (index < 0)
                {
                    return false;
                }

                data.Products[index] = product.ToCopy();
                return true;
            }
        }

        public void DeleteProduct(int id)
        {
            lock (syncRoot)
            {
                var product = data.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    data.Products.Remove(product);
                }
            }
        }

        public List<BulkPriceEditItem> GetBulkPriceEditItems(ProductCategory type)
        {
            lock (syncRoot)
            {
                return data.Products
                    .Where(p => p.Type == type)
                    .Select(p => new BulkPriceEditItem
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToList();
            }
        }

        public void UpdateProductPrices(ProductCategory type, IEnumerable<BulkPriceEditItem> products)
        {
            lock (syncRoot)
            {
                foreach (var product in products)
                {
                    var existing = data.Products.FirstOrDefault(p => p.Id == product.Id && p.Type == type);
                    if (existing != null)
                    {
                        existing.Price = product.Price;
                    }
                }
            }
        }
    }
}
