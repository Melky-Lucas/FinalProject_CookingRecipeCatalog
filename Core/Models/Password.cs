using Core.Base;

namespace Core.Models
{
    public class Password : BaseEntity
    {
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}
