using Squash.WebAPI.Models.DTOs.User;

namespace Squash.WebAPI.Models.DTOs.Url
{
    public class UrlCreateDTO
    {
        public required string BaseUrl { get; set; }
        public required string Alias { get; set; }
        public required int UserId { get; set; }
        public required string Description { get; set; }
        public required string Flag { get; set; }
    }
}
