using Squash.WebAPI.Interfaces.Services.Base;
using Squash.WebAPI.Models;
using Squash.WebAPI.Models.DTOs.User;

namespace Squash.WebAPI.Interfaces.Services
{
    /// <summary>
    /// Interface for the User service.
    /// </summary>
    public interface IUserService : IService<User>
    {
        Task<User?> GetOrCreateUserAsync(User user);

    }
}
