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
    public class EFProductRepository : IProductRepository
    {
        private readonly EfDbContext _context;

        public EFProductRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Product entity)
        {
            entity.CreatedDate = DateTime.Now;
            await _context.Products.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task SoftDelete(int id)
        {
            var product = _context.Products.Find(id);
            product.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Product>> GetAllEntities()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetEntityById(int id)
        {
            var product = await _context.Products.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Update(Product entity)
        {
            _context.Products.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<Product> GetByName(string name)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return product;
        }
    }
}
