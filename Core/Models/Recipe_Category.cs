using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Recipe_Category
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int RecipeId { get; set; }
        public RecipeCategory Category { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
