namespace Squash.WebAPI.Models
{
    public class Url
    {
        public required int Id { get; set; } 
        public required Guid Guid { get; set; } 
        public required string BaseUrl { get; set; } 
        public required string ShortUrl { get; set; } 
        public required int UserId { get; set; } 
        public required User User { get; set; }
        public required int VisitCount { get; set; } = 0;
        public required DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
