using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task SoftDelete(int id);
        Task<Product> GetByName(string name);
    }
}
