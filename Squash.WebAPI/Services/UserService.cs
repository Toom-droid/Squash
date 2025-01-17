﻿using Squash.WebAPI.Interfaces.Repositories;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Models;
using Squash.WebAPI.Repositories;

namespace Squash.WebAPI.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        private readonly IUserRepository _repository = repository;

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

        public async Task<User?> GetOrCreateUserAsync(User user)
        {
            var existingUser = await _repository.GetByAuthMethodIdAsync(user.AuthMethodId);

            if (existingUser != null)
            {
                return existingUser;
            }

            var isCreated = await _repository.CreateAsync(user);
            if (!isCreated)
            {
                return null;
            }

            return await _repository.GetByAuthMethodIdAsync(user.AuthMethodId);
        }
    }
}
