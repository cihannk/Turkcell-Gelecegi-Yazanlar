using MarketApp.Business.Abstract;
using MarketApp.Dtos.Request;
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

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(product);
                return Ok("Ürün başarıyla güncellendi");
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProductRequest product)
        {
            if (ModelState.IsValid)
            {
                int productId = await _productService.AddProduct(product);
                return CreatedAtAction(nameof(GetById),routeValues: new {id= productId}, null);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
           await _productService.RemoveProduct(id);
           return Ok("Product başarıyla silindi");
        }
    }
}
