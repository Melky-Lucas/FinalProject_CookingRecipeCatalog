using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Recipe_Category
    {
        public int CategoryId { get; set; }
        public int RecipeId { get; set; }

        // Navigation properties
        public RecipeCategory Category { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
