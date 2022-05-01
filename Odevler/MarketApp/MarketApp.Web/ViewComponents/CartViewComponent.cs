using MarketApp.Web.Extensions;
using MarketApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.Web.ViewComponents
{
    public class CartViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = HttpContext.Session.GetJson<CartCollection>("cart");
            return View(cart);
        }
    }
}
