using System.ComponentModel.DataAnnotations.Schema;

namespace Squash.WebAPI.Models
{
    [Table("Users")]
    public class User
    {
        public required int Id { get; set; }
        public required Guid Guid { get; set; }
        public required string Email { get; set; } 
        public required string Name { get; set; }
        public required string AuthMethod { get; set; }
        public required string AuthMethodId { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
