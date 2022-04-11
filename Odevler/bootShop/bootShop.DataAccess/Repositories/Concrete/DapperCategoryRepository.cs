using bootShop.DataAccess.Data;
using bootShop.DataAccess.Repositories.Abstract;
using bootShop.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.DataAccess.Repositories.Concrete
{
    public class DapperCategoryRepository : ICategoryRepository
    {
        private DapperDbContext _context;

        public DapperCategoryRepository(DapperDbContext context)
        {
            _context = context;
        }
        public Task<int> Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllEntities()
        {
            var query = "SELECT * FROM Categories";
            using (var connection = _context.CreateConnection())
            {
                var categories = await connection.QueryAsync<Category>(query);
                return categories.ToList();
            }
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
