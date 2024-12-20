namespace Squash.WebAPI.Models.DTOs.User
{
    public class UserUpdateDTO
    {
        public required int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
