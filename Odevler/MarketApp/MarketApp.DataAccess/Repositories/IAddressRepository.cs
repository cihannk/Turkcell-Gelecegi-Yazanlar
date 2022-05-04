using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories
{
    public interface IAddressRepository: IRepository<Address>
    {
        Task<IList<Address>> GetAllEntitiesByUserId(int userId);
    }
}
