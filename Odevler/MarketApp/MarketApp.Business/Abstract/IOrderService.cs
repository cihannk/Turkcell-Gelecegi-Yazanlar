using MarketApp.Dtos.Request;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface IOrderService
    {
        Task BeginOrder(AddOrderRequest order);
        Task<IList<Order>> GetAllOrders();
        Task<IList<Order>> GetOrdersByUserId(int id);
        Task ClearAllCartItemsInOrder(int orderId);
        Task<Order> GetOrderById(int id);
        Task UpdateOrder(UpdateOrderRequest order);
        Task DeleteOrder(int id);
    }
}
