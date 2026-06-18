using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public required string Email { get; set; }
        public required int PasswordId { get; set; }

        // Navigation properties
        public Password Password { get; set; } = null!;
        public ICollection<Recipe> Recipes { get; set; } = [];
        public ICollection<Tip> Tips { get; set; } = [];
    }
}
