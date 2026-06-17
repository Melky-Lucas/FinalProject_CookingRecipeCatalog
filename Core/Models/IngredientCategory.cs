using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class IngredientCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Navigation property
        public ICollection<Ingredient> Ingredients { get; set; } = [];
    }
}
