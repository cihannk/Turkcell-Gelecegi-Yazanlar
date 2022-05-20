using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories.Abstract
{
    public interface IRoleRepository: IRepository<Role>
    {
        Task<bool> IsNameExist(string name);
    }
}
