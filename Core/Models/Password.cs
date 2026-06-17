using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Password
    {
        public int Id { get; set; }
        public required string PasswordHash { get; set; }
        public required string Salt { get; set; }
        public User User { get; set; } = null!;
    }
}
