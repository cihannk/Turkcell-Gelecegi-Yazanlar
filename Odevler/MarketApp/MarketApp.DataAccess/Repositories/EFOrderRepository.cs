using MarketApp.DataAccess.Contexts;
using MarketApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories
{
    internal class EFOrderRepository : IOrderRepository
    {
        private readonly EfDbContext _context;

        public EFOrderRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Order>> GetAllEntities()
        {
            return await _context.Orders.Include(x => x.CartItems).ToListAsync();
        }

        public async Task<IList<Order>> GetEntitesByUserId(int userId)
        {
            return _context.Orders.Where(x => x.UserId == userId).ToList();
        }

        public async Task<Order> GetEntityById(int id)
        {
            return await _context.Orders.Include(x => x.CartItems).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Orders.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Update(Order entity)
        {
            _context.Orders.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
