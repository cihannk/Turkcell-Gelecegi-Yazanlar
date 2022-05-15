using MarketApp.DataAccess.Contexts;
using MarketApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories
{
    public class EFRoleRepository : IRoleRepository
    {
        private readonly EfDbContext _context;

        public EFRoleRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Role entity)
        {
            await _context.Roles.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Role>> GetAllEntities()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetEntityById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Roles.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsNameExist(string name)
        {
            return await _context.Roles.AnyAsync(x => x.Name == name);
        }

        public async Task<int> Update(Role entity)
        {
            _context.Roles.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
