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
    public class EFAddressRepository : IAddressRepository
    {
        private readonly EfDbContext _context;

        public EFAddressRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Address entity)
        {
            await _context.Addresses.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Address>> GetAllEntities()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<IList<Address>> GetAllEntitiesByUserId(int userId)
        {
            return await _context.Addresses.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Address> GetByDesiredName(string name)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.DesiredName == name);
        }

        public async Task<Address> GetEntityById(int id)
        {
            return await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Addresses.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Update(Address entity)
        {
            _context.Addresses.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
