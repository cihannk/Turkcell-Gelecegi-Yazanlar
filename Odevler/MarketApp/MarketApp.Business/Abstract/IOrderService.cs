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
        Task BeginOrder(Order order);
        Task<IList<Order>> GetAllOrders();
        Task<IList<Order>> GetOrdersByUserId(int id);
        Task<CartItem> CreateCartItem(int productId, int amount, double pastPrice);
    }
}
