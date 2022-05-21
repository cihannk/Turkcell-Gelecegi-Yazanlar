using MarketApp.API.Filters;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.SuccessMessages;
using MarketApp.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MarketApp.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [ResponseCache(CacheProfileName ="Category")]
        public async Task<IActionResult> GetAll()
        {
            // default caching mechanism

            //var result = await _memoryCache.GetOrCreateAsync("getCategories", async entry =>
            //{
            //    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(15);
            //    return await _categoryService.GetCategories();
            //});

            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "Category")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryEntity(id);
            return Ok(category);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ModelValidation]
        public async Task<IActionResult> Update(UpdateCategoryRequest category)
        {
            await _categoryService.UpdateCategory(category);
            return Ok(SuccessMessages.Category.SuccessfullyUpdated);         
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ModelValidation]
        public async Task<IActionResult> Create(AddCategoryRequest category)
        {
            int categoryId = await _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = categoryId }, null);
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
