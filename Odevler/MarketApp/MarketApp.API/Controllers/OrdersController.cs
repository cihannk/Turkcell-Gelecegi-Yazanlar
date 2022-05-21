using Hangfire;
using MarketApp.API.Extensions;
using MarketApp.API.Filters;
using MarketApp.API.Models;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.ErrorMessages;
using MarketApp.Business.Constants.SuccessMessages;
using MarketApp.Business.Data;
using MarketApp.Dtos.Request;
using MarketApp.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin,Moderator")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }
        [ModelValidation]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateOrderRequest order)
        {
            await _orderService.ClearAllCartItemsInOrder(order.Id);
            await _orderService.UpdateOrder(order);
            return Ok(SuccessMessages.Order.SuccessfullyUpdated);
        }
        [ModelValidation]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AddOrderRequest order)
        {
            await _orderService.BeginOrder(order);
            return Ok(SuccessMessages.Order.SuccessfullyCreated);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrder(id);
            return Ok(SuccessMessages.Order.SuccessfullyDeleted);
        }
        //Controllers for user
        [ModelValidation]
        [HttpPost("User")]
        [Authorize]
        public async Task<IActionResult> Order(OrderPaymentModel orderPaymentModel)
        {
            if(await _paymentService.Processing(orderPaymentModel.PaymentProcessingInfo))
            {
                await _orderService.BeginOrder(new AddOrderRequest { UserId= User.Identity.GetId(), AddressId= orderPaymentModel.AddressId, CartItems= orderPaymentModel.CartItems});
                BackgroundJob.Enqueue(() => ThanksForOrder());
                return Ok(SuccessMessages.Order.SuccessfullyCreatedUserOrder);
            }
            return BadRequest(ErrorMessages.Order.PaymentFail);
        }
        [HttpGet("User")]
        [Authorize]
        public async Task<IActionResult> GetUserOrders()
        {
            var orders = await _orderService.GetOrdersByUserId(User.Identity.GetId());
            return Ok(orders);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void ThanksForOrder()
        {
            // Email imitation
            Console.WriteLine("Thanks for your order");
        }
    }
}
