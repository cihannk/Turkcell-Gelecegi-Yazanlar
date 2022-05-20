using MarketApp.Business.Abstract;
using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using MarketApp.Entities;
using MarketApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketApp.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsWithoutCheckingActive();
            return View(products);
        }
        public async Task<IActionResult> Add()
        {
            var categories = getCategories(await _categoryService.GetCategories());
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddProductRequest product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductWithoutCheckingActive(id);
            var categories = getCategories(await _categoryService.GetCategories());
            ViewBag.Categories = categories;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductRequest productRequest)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(productRequest);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteOk(int id)
        {
            await _productService.RemoveProduct(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProduct(id);
            return View(product);
        }

        private IList<SelectListItem> getCategories(IList<GetCategoriesResponse> categories)
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in categories)
            {
                items.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }
            return items;
        }

    }
}
