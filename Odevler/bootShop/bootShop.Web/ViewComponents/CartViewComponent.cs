using bootShop.Web.Extensions;
using bootShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace bootShop.Web.ViewComponents
{
    public class CartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var collection = HttpContext.Session.GetJson<CartCollection>("sepet");
            return View(collection);
        }
    }
}
