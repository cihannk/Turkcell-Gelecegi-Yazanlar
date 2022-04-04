using bootShop.DataAccess.Data;
using bootShop.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.DataAccess.Repositories
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly DapperDbContext _context;
        public DapperProductRepository(DapperDbContext context)
        {
            _context = context;
        }
        public Task<int> Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllEntities()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public Task<Product> GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
