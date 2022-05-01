using bootShop.Business;
using bootShop.Dtos.Requests;
using bootShop.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{ 
    [Authorize(Roles = "admin,editor")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await GetCategoriesForDropDown();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProductRequest request)
        {
            if (ModelState.IsValid)
            {
                int addedProductId = await _productService.AddProduct(request);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private async Task<List<SelectListItem>> GetCategoriesForDropDown()
        {
            var categories = await _categoryService.GetCategories();
            var selectListItems = new List<SelectListItem>();
            
            foreach(var cat in categories)
            {
                selectListItems.Add(new SelectListItem { Text = cat.Name, Value = cat.Id.ToString() });
            }
            return selectListItems;
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (await _productService.IsExist(id))
            {
                var product = await _productService.GetProductById(id);
                ViewBag.Categories = await GetCategoriesForDropDown();
                return View(product);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductRequest request)
        {
            if (ModelState.IsValid)
            {
                int result = await _productService.UpdateProduct(request);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (await _productService.IsExist(id))
            {
                var product = await _productService.GetProductById(id);
                return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteOk(int id)
        {
            await _productService.SoftDeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
