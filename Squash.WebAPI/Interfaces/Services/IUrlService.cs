using Squash.WebAPI.Interfaces.Services.Base;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Interfaces.Services
{
    /// <summary>
    /// Interface for the Url service.
    /// </summary>
    public interface IUrlService : IService<Url>
    {
        Task<bool> UrlAliasExistsAsync(string alias, int userId);
        Task<Url> GetUrlByAliasAync(string alias, int userId);
        Task<IEnumerable<Url>> GetUrlsByUserIdAsync(int userId);
    }
}
