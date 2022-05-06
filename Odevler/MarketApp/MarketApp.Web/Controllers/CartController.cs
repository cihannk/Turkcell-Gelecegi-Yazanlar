using MarketApp.Business.Abstract;
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
        [TempData]
        public int addressId {get;set;}

        [TempData]
        public string TotalAmount { get; set; }

        public CartController(IProductService productService, IOrderService orderService, IAddressService addressService)
        {
            _productService = productService;
            _orderService = orderService;
            _addressService = addressService;
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
            var addresses = await _addressService.GetUserAddressesWithUserId(userId);
            ViewBag.Addresses = addresses;
            return View(cartCollection);
        }
        [Authorize]
        public IActionResult Checkout(int addressId)
        {
            var cartCollection = HttpContext.Session.GetJson<CartCollection>("cart");
            this.addressId = addressId;   
            ViewBag.DollarAmount = cartCollection.GetTotal();
            ViewBag.Total = (int)Math.Round(ViewBag.DollarAmount, 2) * 100;
            ViewBag.Email = HttpContext.User.Identity.GetEmail() ?? "example@example.com";
            TotalAmount = ViewBag.Total.ToString();
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Processing(string stripeToken, string stripeEmail)
        {
            var cartCollection = HttpContext.Session.GetJson<CartCollection>("cart");
            var customerOpts = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = HttpContext.User.Identity.Name ?? "Cihan"
            };
            var customerService = new CustomerService();
            Customer customer = customerService.Create(customerOpts);
            var optionsCharge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(TempData["TotalAmount"]),
                Currency = "TRY",
                Description = "User cart",
                Source = stripeToken,
                ReceiptEmail = stripeEmail
            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status == "succeeded")
            {
                await _orderService.BeginOrder(new Entities.Order { AddressId = addressId, CartItems = createAllCartItems(cartCollection), OrderDate = DateTime.Now, UserId = User.Identity.GetId()});
                ViewBag.AmountPaid = Convert.ToDecimal(charge.Amount);
                ViewBag.Customer = customer.Name;
                cartCollection.ClearCart();
                saveToSession(cartCollection);
                return View();
            }
            else
            {
                return Json("Db sorunu");
            }
        }

        private CartCollection getCollectionFromSession()
        {
            var cart = HttpContext.Session.GetJson<CartCollection>("cart");
            return cart ?? new CartCollection();
        }

        private List<Entities.CartItem> createAllCartItems(CartCollection collection)
        {
            List<Entities.CartItem> cartItems = new List<Entities.CartItem>();
            foreach(var collectionCartItem in collection.CartItems) {
                var result = new Entities.CartItem { ProductId = collectionCartItem.Product.Id, Amount = collectionCartItem.Quantity, PastPrice= (collectionCartItem.Product.Price * (1 - collectionCartItem.Product.Discount))};
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
