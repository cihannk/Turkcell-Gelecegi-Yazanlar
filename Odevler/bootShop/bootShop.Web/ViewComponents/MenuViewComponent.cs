using bootShop.Business;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bootShop.Web.ViewComponents
{
    public class MenuViewComponent: ViewComponent
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
