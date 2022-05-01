using MarketApp.Business;
using MarketApp.Business.Abstract;
using MarketApp.Dtos.Response;
using MarketApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
 

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? categoryName, int page=1)
        {
            IList<GetProductsResponse> products;
            int productsPerPage = 8;
            ViewBag.Page = page;
            if (categoryName != null)
            {
                products = await _productService.GetProductsByCategoryName(categoryName);
            }
            else
            {
                products = await _productService.GetProducts();
            }
            ViewBag.PageCount = Math.Ceiling((decimal)products.Count / (decimal)productsPerPage);
            products = products.OrderBy(p => p.Id).Skip((page - 1) * productsPerPage).Take(productsPerPage).ToList();
            
            return View(products);
        }
    }
}