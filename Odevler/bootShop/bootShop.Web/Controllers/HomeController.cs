using bootShop.Business;
using bootShop.Entities;
using bootShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? categoryName, int page = 1)
        {

            var products = await _productService.GetProducts();
            if (categoryName != null)
            {
                var categories =  await _categoryService.GetCategories();
                var selectedCategory = categories.FirstOrDefault(cat => cat.Name == categoryName);
                products = products.Where(product => product.CategoryId == selectedCategory.Id).ToList();
            }
            var productsPerPage = 3;
            var paginatedProducts = products.OrderBy(x => x.Id).Skip((page-1) * productsPerPage).Take(productsPerPage);
            ViewBag.TotalPages = Math.Ceiling((decimal)products.Count() / productsPerPage);
            ViewBag.Page = page;

            return View(paginatedProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
