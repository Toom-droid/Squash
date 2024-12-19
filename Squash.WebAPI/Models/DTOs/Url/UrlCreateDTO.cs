using Squash.WebAPI.Models.DTOs.User;

namespace Squash.WebAPI.Models.DTOs.Url
{
    public class UrlCreateDTO
    {
        public required Guid Guid { get; set; }
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public required int UserId { get; set; }
    }
}
