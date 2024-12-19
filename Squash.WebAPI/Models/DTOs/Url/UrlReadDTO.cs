using Squash.WebAPI.Models.DTOs.User;

namespace Squash.WebAPI.Models.DTOs.Url
{
    public class UrlReadDTO
    {
        public required int Id { get; set; }
        public required Guid Guid { get; set; }
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public required int UserId { get; set; }
        public required UserReadDTO User { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required int VisitCount { get; set; } 
    }
}
