using Squash.WebAPI.Interfaces.Repositories.Base;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Interfaces.Repositories
{
    /// <summary>
    /// Interface for the Url repository.
    /// </summary>
    public interface IUrlRepository : IRepository<Url>
    {
        Task<bool> UrlAliasExistsAsync(string alias);
        Task<bool> UrlAliasExistsByIdAsync(string alias, int urlId);
        Task<Url> GetUrlByAliasAync(string alias);
        Task<IEnumerable<Url>> GetUrlsByUserIdAsync(int userId);
        Task<bool> UpdateUrlVisitCountAsync(int urlId, int visitCount);
    }
}
