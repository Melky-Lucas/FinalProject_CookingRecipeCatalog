using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Tips
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Content { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
