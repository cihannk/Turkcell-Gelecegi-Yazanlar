using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetEntityById(int id);
        Task<IList<T>> GetAllEntities();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task Delete(int id);
        Task<bool> IsExist(int id);
    }
}
