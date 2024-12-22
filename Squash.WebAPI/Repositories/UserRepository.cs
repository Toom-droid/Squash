using Microsoft.EntityFrameworkCore;
using Squash.WebAPI.Data;
using Squash.WebAPI.Interfaces.Repositories;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Repositories
{
    public class UserRepository(SquashDBContext context) : IUserRepository
    {
        private readonly SquashDBContext _context = context;

        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task<bool> CreateAsync(User user)
        {
            user.Guid = Guid.NewGuid();
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync() == 1;
            }
            else
            {
                return false;
            }
        }

        public async Task<User?> GetByAuthMethodIdAsync(string authMethodId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.AuthMethodId == authMethodId);
        }
    }
}
