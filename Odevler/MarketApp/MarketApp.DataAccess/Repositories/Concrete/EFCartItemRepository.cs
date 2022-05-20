using MarketApp.DataAccess.Contexts;
using MarketApp.DataAccess.Repositories.Abstract;
using MarketApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories.Concrete
{
    public class EFCartItemRepository : ICartItemRepository
    {
        private readonly EfDbContext _context;

        public EFCartItemRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(CartItem entity)
        {
            await _context.CartItems.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<CartItem>> GetAllEntities()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetEntityById(int id)
        {
            return await _context.CartItems.FindAsync(id);
        }

        public async Task<CartItem> GetExistCartItem(int productId, int amount)
        {
            return await _context.CartItems.FirstOrDefaultAsync(x => x.ProductId == productId && x.Amount == amount);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.CartItems.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Update(CartItem entity)
        {
            _context.CartItems.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
