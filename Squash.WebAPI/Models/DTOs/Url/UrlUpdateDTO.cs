namespace Squash.WebAPI.Models.DTOs.Url
{
    public class UrlUpdateDTO
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public string? BaseUrl { get; set; }
        public string? Alias { get; set; }
        public string? Description { get; set; }
        public string? Flag { get; set; }
        public int? VisitCount { get; set; }
    }
}
