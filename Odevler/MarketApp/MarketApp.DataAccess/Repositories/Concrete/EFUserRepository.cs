using MarketApp.DataAccess.Contexts;
using MarketApp.DataAccess.Repositories.Abstract;
using MarketApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess.Repositories.Concrete
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
            return await _context.Users.Include(user => user.Role).ToListAsync();
        }

        public async Task<User> GetEntityByEmail(string email)
        {
            return await _context.Users.Include(u =>u.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetEntityById(int id)
        {
            return await _context.Users.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetEntityByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        public async Task<int> Update(User entity)
        {
            _context.Users.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
