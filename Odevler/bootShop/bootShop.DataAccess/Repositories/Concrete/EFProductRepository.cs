using bootShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.DataAccess.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly bootShopDbContext _context;
        public EFProductRepository(bootShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllEntities()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetEntityById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<bool> IsExist(int id)
        {
            var isExist = await _context.Products.AnyAsync(x => x.Id == id);
            return isExist;
        }

        public async Task<IEnumerable<Product>> SearchProductsByName(string name)
        {
            var products = await _context.Products.Where(product => product.Name.Contains(name)).ToListAsync();
            return products;
        }

        public async Task SoftDelete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Product entity)
        {
            _context.Products.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
