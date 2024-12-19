namespace Squash.WebAPI.Models.DTOs.User
{
    public class UserCreateDTO
    {
        public required Guid Guid { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string AuthMethod { get; set; }
        public required string AuthMethodId { get; set; }
    }
}
