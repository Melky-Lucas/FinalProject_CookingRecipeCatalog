using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public required int PasswordId { get; set; }

        // Navigation properties
        public Password Password { get; set; } = null!;
        public ICollection<Recipe> Recipes { get; set; } = [];
        public ICollection<Tips> Tips { get; set; } = [];
    }
}
