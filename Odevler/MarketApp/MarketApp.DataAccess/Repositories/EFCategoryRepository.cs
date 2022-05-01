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
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EfDbContext _context;

        public EFCategoryRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Category>> GetAllEntities()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category> GetByName(string name)
        {
            var result = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return result;
        }

        public async Task<Category> GetEntityById(int id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<int> Update(Category entity)
        {
            _context.Categories.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
