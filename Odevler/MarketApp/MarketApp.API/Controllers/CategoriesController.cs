using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.SuccessMessages;
using MarketApp.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Roles ="Admin,Moderator")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryEntity(id);
            return Ok(category);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateCategoryRequest category)
        {
            await _categoryService.UpdateCategory(category);
            return Ok(SuccessMessages.Category.SuccessfullyUpdated);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AddCategoryRequest category)
        {
            await _categoryService.AddCategory(category);
            return Ok(SuccessMessages.Category.SuccessfullyCreated);
        }
        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await _categoryService.RemoveCategory(categoryId);
            return Ok(SuccessMessages.Category.SuccessfullyDeleted);
        }
    }
}
