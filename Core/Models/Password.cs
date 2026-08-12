using Core.Base;

namespace Core.Models
{
    public class Password : BaseEntity
    {
        public required string PasswordHash { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}
