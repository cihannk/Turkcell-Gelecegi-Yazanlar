using MarketApp.Business.Abstract;
using MarketApp.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddCategoryRequest category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(category);
                return Redirect("/");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int categoryId)
        {
            await _categoryService.RemoveCategory(categoryId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(request);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
