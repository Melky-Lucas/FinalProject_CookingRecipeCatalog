using Core.Base;

namespace Core.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null!;
        public required string Email { get; set; }
        public required int PasswordId { get; set; }
        public required int RoleId { get; set; }

        // Navigation properties
        public Password Password { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public ICollection<Recipe> Recipes { get; set; } = [];
        public ICollection<Tip> Tips { get; set; } = [];
    }
}
