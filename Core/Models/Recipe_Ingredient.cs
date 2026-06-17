using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Recipe_Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        public bool IsOptional { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public Ingredient Ingredient { get; set; } = null!;
        public MeasureUnit Unit { get; set; } = null!;
    }
}
