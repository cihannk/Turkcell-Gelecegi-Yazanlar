using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<bool> IsEmailExist(string email);
        Task<User> GetEntityByEmail(string email);
    }
}
