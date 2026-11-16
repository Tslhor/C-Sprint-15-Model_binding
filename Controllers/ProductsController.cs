using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductsValidation.Models;
using ProductsValidation.Services;

namespace ProductsValidation.Controllers
{
    public class ProductsController : Controller
    {
        private const string NotExistsViewPath = "~/Views/Shared/NotExists.cshtml";
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        
        public IActionResult Index(ProductCategory? type, int page = 1)
        {
            const int pageSize = 5;
            var products = productService.GetProducts(type);

            var totalItems = products.Count;
            var totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)pageSize));
            var currentPage = Math.Min(Math.Max(page, 1), totalPages);

            var pagedProducts = products
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new ProductsIndexViewModel
            {
                Products = pagedProducts,
                Type = type,
                CurrentPage = currentPage,
                TotalPages = totalPages
            };

            ViewData["SelectedType"] = type;
            return View(model);
        }
        
        public IActionResult View(int id)
        {
            var product = productService.GetProduct(id);
            if (product != null)
            {
                return View(product);
            }

            return View(NotExistsViewPath);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = productService.GetProduct(id);
            if (product != null)
            {
                return View(product);
            }

            return View(NotExistsViewPath);
        } 

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (!productService.UpdateProduct(product))
            {
                return View(NotExistsViewPath);
            }

            return RedirectToAction(nameof(View), new { id = product.Id });
        }

        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            productService.AddProduct(product);

            return RedirectToAction(nameof(View), new { id = product.Id });
        }

        public IActionResult Create()
        {
            var nextId = productService.GetNextProductId();

            return View(new Product { Id = nextId });
        }

        public IActionResult Delete(int id)
        {
            productService.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditPrices(ProductCategory type)
        {
            var model = new BulkPriceEditViewModel
            {
                Type = type,
                Products = productService.GetBulkPriceEditItems(type)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditPrices(BulkPriceEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Products == null)
            {
                ModelState.AddModelError(nameof(model.Products), "Products payload is required.");
                model.Products = new List<BulkPriceEditItem>();
                return View(model);
            }

            productService.UpdateProductPrices(model.Type, model.Products);

            return RedirectToAction(nameof(Index), new { type = model.Type });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
