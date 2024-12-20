using Squash.WebAPI.Interfaces.Repositories;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Services
{
    public class UrlService(IUrlRepository repository) : IUrlService
    {
        private readonly IUrlRepository _repository = repository;
        public async Task<IEnumerable<Url>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Url> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<bool> CreateAsync(Url url)
        {
            url.Guid = Guid.NewGuid();
            return await _repository.CreateAsync(url);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<bool> UpdateAsync(Url url)
        {
            return await _repository.UpdateAsync(url);
        }
        public async Task<bool> UrlAliasExistsAsync(string alias, int userId)
        {
            return await _repository.UrlAliasExistsAsync(alias, userId);
        }
        public async Task<Url> GetUrlByAliasAync(string alias, int userId)
        {
            return await _repository.GetUrlByAliasAync(alias, userId);
        }

    }
}
