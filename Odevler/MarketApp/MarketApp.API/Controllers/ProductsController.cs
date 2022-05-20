using MarketApp.API.Filters;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.SuccessMessages;
using MarketApp.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProduct(id);
            return Ok(product);
        }
        [ModelValidation]
        [Authorize(Roles ="Admin")]
        [HttpPut]
        [ModelValidation]
        public async Task<IActionResult> Update(UpdateProductRequest product)
        {

            await _productService.UpdateProduct(product);
            return Ok(SuccessMessages.Product.SuccessfullyUpdated);

        }
        [ModelValidation]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ModelValidation]
        public async Task<IActionResult> Create(AddProductRequest product)
        {

            int productId = await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetById),routeValues: new {id= productId}, null);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
           await _productService.RemoveProduct(id);
           return Ok(SuccessMessages.Product.SuccessfullyDeleted);
        }
    }
}
