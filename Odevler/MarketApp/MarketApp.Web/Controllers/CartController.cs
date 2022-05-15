using MarketApp.Business.Abstract;
using MarketApp.Business.Data;
using MarketApp.Dtos.Request;
using MarketApp.Entities;
using MarketApp.Web.Extensions;
using MarketApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace MarketApp.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly IPaymentService _paymentService;

        [TempData]
        public int addressId {get;set;}

        [TempData]
        public string TotalAmount { get; set; }

        public CartController(IProductService productService, IOrderService orderService, IAddressService addressService, IPaymentService paymentService)
        {
            _productService = productService;
            _orderService = orderService;
            _addressService = addressService;
            _paymentService = paymentService;
        }
        public IActionResult Index()
        {
            CartCollection cartCollection = getCollectionFromSession();
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
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

        public IActionResult Remove(int id)
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

        public IActionResult RemoveOne(int id)
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
        public IActionResult GetCartVM()
        {
            return ViewComponent("Cart");
        }

        [Authorize]
        public async Task<IActionResult> Order()
        {
            var userId = User.Identity.GetId();
            var cartCollection = HttpContext.Session.GetJson<CartCollection>("cart");
            if (cartCollection != null)
            {
                var addresses = await _addressService.GetUserAddressesWithUserId(userId);
                ViewBag.Addresses = addresses;
                return View(cartCollection);
            }
            return RedirectToAction(nameof(Index));
            
        }
        [Authorize]
        public IActionResult Checkout(int addressId)
        {
            var cartCollection = HttpContext.Session.GetJson<CartCollection>("cart");
            if (cartCollection != null)
            {
                this.addressId = addressId;
                ViewBag.DollarAmount = Math.Round(cartCollection.GetTotal(), 2);
                ViewBag.Total = (int)ViewBag.DollarAmount* 100;
                ViewBag.Email = HttpContext.User.Identity.GetEmail() ?? "example@example.com";
                TotalAmount = ViewBag.Total.ToString();
                return View();
            }
            return RedirectToAction(nameof(Index));
            
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Processing(string stripeToken, string email)
        {
            if (stripeToken == null)
            {
                return Json("Token is not given");
            }

            var cartCollection = HttpContext.Session.GetJson<CartCollection>("cart");

            PaymentProcessingInfos paymentInfo = new PaymentProcessingInfos {
                Amount = ((long)Math.Round(cartCollection.GetTotal(), 2) * 100),
                Currency = "TRY",
                CustomerEmail = email,
                Description = "MarketApp Alışverişi",
                Name = User.Identity.Name,
                Token = stripeToken
            };

            bool isSucceeded = await _paymentService.Processing(paymentInfo);
            if (isSucceeded)
            {
                var orderRequest = new AddOrderRequest { UserId = User.Identity.GetId(), AddressId = addressId, CartItems = createAllCartItems(cartCollection) };
                await _orderService.BeginOrder(orderRequest);

                ViewBag.AmountPaid = Convert.ToDecimal(paymentInfo.Amount);
                ViewBag.Customer = paymentInfo.Name;
                cartCollection.ClearCart();
                saveToSession(cartCollection);
                return View();
            }
            else
            {
                return Json("Payment failed");
            }
        }

        private CartCollection getCollectionFromSession()
        {
            var cart = HttpContext.Session.GetJson<CartCollection>("cart");
            return cart ?? new CartCollection();
        }

        private List<AddCartItemRequest> createAllCartItems(CartCollection collection)
        {
            List<AddCartItemRequest> cartItems = new List<AddCartItemRequest>();
            foreach(var collectionCartItem in collection.CartItems) {
                var result = new AddCartItemRequest { ProductId = collectionCartItem.Product.Id, Amount = collectionCartItem.Quantity, PastPrice= (collectionCartItem.Product.Price * (1 - collectionCartItem.Product.Discount))};
                cartItems.Add(result);
            }
            return cartItems;
        }

        private void saveToSession(CartCollection collection)
        {
            HttpContext.Session.SetJson("cart", collection);
        }
    }
}
