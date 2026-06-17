using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}
