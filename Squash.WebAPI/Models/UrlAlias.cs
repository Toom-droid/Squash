namespace Squash.WebAPI.Models
{
    public class UrlAlias
    {
        public required int Id { get; set; } 
        public required Guid Guid { get; set; } 
        public required string Alias { get; set; } 
        public required int UrlId { get; set; } 
        public required Url Url { get; set; } 
        public required int UserId { get; set; } 
        public required User User { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
