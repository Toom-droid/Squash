namespace Squash.WebAPI.Models
{
    public class Url
    {
        public required int Id { get; set; } 
        public required Guid Guid { get; set; } 
        public required string OriginalUrl { get; set; } 
        public required string ShortUrl { get; set; } 
        public required int UserId { get; set; } 
        public required User User { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required int VisitCount { get; set; } = 0;
    }
}
