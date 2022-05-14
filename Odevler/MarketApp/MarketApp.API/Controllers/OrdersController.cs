using MarketApp.Business.Abstract;
using MarketApp.Dtos.Request;
using MarketApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public OrdersController(IOrderService orderService, IPaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderRequest order)
        {
            await _orderService.UpdateOrder(order);
            return Ok("Sipariş başarıyla güncellendi");
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddOrderRequest order)
        {
            return Ok("Sipariş başarıyla eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrder(id);
            return Ok("Sipariş iptal edildi");
        }
    }
}
