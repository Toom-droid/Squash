using Squash.WebAPI.Interfaces.Repositories;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        private readonly IUserRepository _repository = repository;

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<bool> CreateAsync(User user)
        {
            user.Guid = Guid.NewGuid();
            return await _repository.CreateAsync(user);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<bool> UpdateAsync(User user)
        {
            return await _repository.UpdateAsync(user);
        }
    }
}
