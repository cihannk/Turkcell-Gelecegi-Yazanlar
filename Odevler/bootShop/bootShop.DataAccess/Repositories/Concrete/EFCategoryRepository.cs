using bootShop.DataAccess.Repositories.Abstract;
using bootShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.DataAccess.Repositories.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly bootShopDbContext _context;

        public EFCategoryRepository(bootShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            int result = await _context.SaveChangesAsync();
            return result;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllEntities()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public Task<Category> GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
