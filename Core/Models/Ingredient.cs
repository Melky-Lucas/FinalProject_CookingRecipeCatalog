using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = null!;
        public int? IngredientCategoryId { get; set; }

        // Navigation property
        public IngredientCategory IngredientCategory { get; set; } = null!;
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set; } = [];
    }
}
