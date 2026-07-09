using Core.Base;

namespace Core.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = [];
    }
}
