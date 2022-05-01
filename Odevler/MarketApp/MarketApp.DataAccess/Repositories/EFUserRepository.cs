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
    public class EFUserRepository : IUserRepository
    {
        private readonly EfDbContext _context;

        public EFUserRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            return await  _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<User>> GetAllEntities()
        {
            return await _context.Users.ToListAsync();
        }

        // TODO find maybe?
        public async Task<User> GetEntityByEmail(string email)
        {
            return await _context.Users.Include(u =>u.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetEntityById(int id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return false;
            return true;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Update(User entity)
        {
            _context.Users.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
