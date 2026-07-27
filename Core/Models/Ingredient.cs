using Core.Base;

namespace Core.Models
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = null!;
        public int IngredientCategoryId { get; set; }

        // Navigation property
        public IngredientCategory IngredientCategory { get; set; } = null!;
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set; } = [];
    }
}
