using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories.Abstract
{
    public interface IAddressRepository: IRepository<Address>
    {
        Task<IList<Address>> GetAllEntitiesByUserId(int userId);
        Task<Address> GetByDesiredName(string name);
    }
}
