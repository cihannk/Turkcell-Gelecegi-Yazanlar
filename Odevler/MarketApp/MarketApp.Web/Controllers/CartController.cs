using MarketApp.Business.Abstract;
using MarketApp.Entities;
using MarketApp.Web.Extensions;
using MarketApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CartController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            CartCollection cartCollection = getCollectionFromSession();
            return View(cartCollection);
        }
        public async Task<IActionResult> Add(int id)
        {
            var product = await _productService.GetProduct(id);
            var collection = getCollectionFromSession();
            collection.Add(new Models.CartItem { Product=product, Quantity=1});

            saveToSession(collection);
            return Json($"{id} sepete eklendi");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var collection = getCollectionFromSession();
            if (collection.IsProductExist(id))
            {
                collection.Delete(id);
                saveToSession(collection);
                return Json($"{id} li ürün sepetten silindi");
            }
            throw new InvalidOperationException("Product is not in cart");
        }

        public async Task<IActionResult> RemoveOne(int id)
        {
            var collection = getCollectionFromSession();
            if (collection.IsProductExist(id))
            {
                if (collection.GetCartItem(id).Quantity > 1)
                {
                    collection.DecreaseAmount(id);
                    saveToSession(collection);
                    return Json($"{id} li ürünün adeti düşürüldü");
                }
                else
                {
                    collection.Delete(id);
                    saveToSession(collection);
                    return Json($"{id} li ürün silindi");
                }
            }
            throw new InvalidOperationException("Product is not in cart");
        }
        public async Task<IActionResult> GetCartVM()
        {
            return ViewComponent("Cart");
        }
        public async Task<IActionResult> Order()
        {
            var cart = HttpContext.Session.GetJson<CartCollection>("cart");
            if (cart == null)
            {
                return Redirect("/");
            }
            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> Order(Order order)
        {
            _orderService.BeginOrder(order);
            return Redirect("/");
        }


        private CartCollection getCollectionFromSession()
        {
            var cart = HttpContext.Session.GetJson<CartCollection>("cart");
            return cart ?? new CartCollection();
        }
        private void saveToSession(CartCollection collection)
        {
            HttpContext.Session.SetJson("cart", collection);
        }

       
    }
}
