using Squash.WebAPI.Interfaces.Repositories.Base;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Interfaces.Repositories
{
    /// <summary>
    /// Interface for the User repository.
    /// </summary>
    public interface IUserRepository : IRepository<User> 
    {
        Task<User?> GetByAuthMethodIdAsync(string authMethodId);
    }
}
