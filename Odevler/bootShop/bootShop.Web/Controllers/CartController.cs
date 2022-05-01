using bootShop.Business;
using bootShop.Web.Extensions;
using bootShop.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        public CartController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var cartCollection = getCollectionFromSession();
            return View(cartCollection);
        }
        public async Task<IActionResult> Add(int id)
        {
            if (await _productService.IsExist(id))
            {
                var product = await _productService.GetProductById(id);
                CartCollection cartCollection = getCollectionFromSession();
                cartCollection.Add(new CartItem { Product= product, Quantity= 1});
                saveToSession(cartCollection);
            }
            else
            {
                return NotFound();
            }

            return Json($"{id} Sepete eklendi");
        }

        private CartCollection getCollectionFromSession()
        {
            var result = HttpContext.Session.GetJson<CartCollection>("sepet");
            return result ?? new CartCollection();
        }
        private void saveToSession(CartCollection cartCollection)
        {
            //string json = JsonConvert.SerializeObject(cartCollection);
            //HttpContext.Session.SetString("sepet", json);
            HttpContext.Session.SetJson("sepet", cartCollection);
        }
    }
}
