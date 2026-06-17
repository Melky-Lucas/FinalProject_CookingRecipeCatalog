using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Tip
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string Content { get; set; } = null!;

        // Navigation properties
        public Recipe Recipe { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
