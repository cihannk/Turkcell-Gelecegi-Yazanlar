using MarketApp.Dtos.Request;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface IRoleService
    {
        Task<IList<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<int> AddRole(AddRoleRequest role);
        Task DeleteRole(int id);
        Task UpdateRole(Role role);
    }
}
