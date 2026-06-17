using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required int PasswordId { get; set; }
        public Password Password { get; set; } = null!;
        public ICollection<Recipe> Recipes { get; set; } = [];
    }
}
