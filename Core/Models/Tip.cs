using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Tip
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int RecipeId { get; set; }
        public required string Content { get; set; }

        // Navigation properties
        public Recipe Recipe { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
