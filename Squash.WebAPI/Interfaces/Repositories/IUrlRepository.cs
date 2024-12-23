﻿using Squash.WebAPI.Interfaces.Repositories.Base;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Interfaces.Repositories
{
    /// <summary>
    /// Interface for the Url repository.
    /// </summary>
    public interface IUrlRepository : IRepository<Url>
    {
        Task<bool> UrlAliasExistsAsync(string alias, int userId);
        Task<Url> GetUrlByAliasAync(string alias, int userId);
        Task<IEnumerable<Url>> GetUrlsByUserIdAsync(int userId);
        Task<bool> UpdateUrlVisitCountAsync(int userId, int urlId, int visitCount);
    }
}
