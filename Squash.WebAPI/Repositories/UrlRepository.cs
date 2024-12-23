using Squash.WebAPI.Data;
using Squash.WebAPI.Interfaces.Repositories;
using Squash.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;


namespace Squash.WebAPI.Repositories
{
    public class UrlRepository(SquashDBContext context) : IUrlRepository
    {
        private readonly SquashDBContext _context = context;
        public async Task<Url> GetByIdAsync(int id) => await _context.Urls.Include(u => u.User).FirstOrDefaultAsync(u => u.Id == id);
        public async Task<IEnumerable<Url>> GetAllAsync() => await _context.Urls.Include(u => u.User).ToListAsync();
        public async Task<bool> CreateAsync(Url url)
        {
            await _context.Urls.AddAsync(url);
            return await _context.SaveChangesAsync() == 1;
        }
        public async Task<bool> UpdateAsync(Url url)
        {
            var response = await _context.Urls
            .Where(u => u.UserId == url.UserId && u.Id == url.Id)
                .ExecuteUpdateAsync(properties => properties
                    .SetProperty(u => u.BaseUrl, url.BaseUrl)
                    .SetProperty(u => u.Alias, url.Alias)
                    .SetProperty(u => u.Description, url.Description));
            return response > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var url = await _context.Urls.FindAsync(id);
            if (url != null)
            {
                _context.Urls.Remove(url);
                return await _context.SaveChangesAsync() == 1;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UrlAliasExistsAsync(string alias) => await _context.Urls.AnyAsync(u => u.Alias == alias);
        public async Task<bool> UrlAliasExistsByIdAsync(string alias, int urlId) => await _context.Urls.AnyAsync(u => u.Alias == alias && u.Id != urlId);
        public async Task<Url> GetUrlByAliasAync(string alias, int userId) => await _context.Urls.FirstOrDefaultAsync(u => u.Alias == alias && u.UserId == userId);
        public async Task<IEnumerable<Url>> GetUrlsByUserIdAsync(int userId) => await _context.Urls.Where(u => u.UserId == userId).ToListAsync();
        public async Task<bool> UpdateUrlVisitCountAsync(int userId, int urlId, int visitCount)
        {   
            var response = await _context.Urls
                .Where(u => u.UserId == userId && u.Id == urlId)
             .ExecuteUpdateAsync(properties => properties
                 .SetProperty(u => u.VisitCount, visitCount));
            return response > 0;
        }
    }
}
