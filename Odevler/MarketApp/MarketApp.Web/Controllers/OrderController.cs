using MarketApp.Business.Abstract;
using MarketApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> Index(Order order)
        {
            await _orderService.BeginOrder(order);
            return Ok();
        }
    }
}
