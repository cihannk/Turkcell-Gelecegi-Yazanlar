using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories.Abstract
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<IList<Order>> GetEntitesByUserId(int userId);
        Task ClearAllCartItems(int orderId);
    }
}
