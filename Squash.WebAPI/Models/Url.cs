namespace Squash.WebAPI.Models
{
    public class Url
    {
        public required int Id { get; set; }
        public required Guid Guid { get; set; } 
        public required string BaseUrl { get; set; } 
        public required string Alias { get; set; }
        public required string Description { get; set; }
        public required string Flag { get; set; }
        public required int UserId { get; set; } 
        public required User User { get; set; }
        public required int VisitCount { get; set; } = 0;
        public required DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
