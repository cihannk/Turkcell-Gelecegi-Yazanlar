using MarketApp.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public MenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }
    }
}
