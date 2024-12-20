namespace Squash.WebAPI.Models.DTOs.Url
{
    public class UrlUpdateDTO
    {
        public required int Id { get; set; }
        public string? BaseUrl { get; set; }
        public string? Alias { get; set; }
    }
}
