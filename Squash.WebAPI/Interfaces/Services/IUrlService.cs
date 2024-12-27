using Squash.WebAPI.Interfaces.Services.Base;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Interfaces.Services
{
    /// <summary>
    /// Interface for the Url service.
    /// </summary>
    public interface IUrlService : IService<Url>
    {
        Task<bool> UrlAliasExistsAsync(string alias);
        Task<bool> UrlAliasExistsByIdAsync(string alias, int urlId);
        Task<Url> GetUrlByAliasAync(string alias);
        Task<IEnumerable<Url>> GetUrlsByUserIdAsync(int userId);
        Task<bool> UpdateUrlVisitCountAsync(int urlId, int visitCount);
    }
}
