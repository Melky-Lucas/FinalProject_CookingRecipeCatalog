using static Core.Models.Enums.ModelEnums;

namespace Core.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int Servings { get; set; }
        public RecipeDifficulty Difficulty { get; set; }
        public int Calories { get; set; }
        public int UserId { get; set; }
        public bool IsPublic { get; set; } = false;

        // Navigation properties
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set; } = [];
        public ICollection<Recipe_Category> Recipe_Categories { get; set; } = [];
        public ICollection<CookingStep> CookingSteps { get; set; } = [];
        public ICollection<Tip> Tips { get; set; } = [];
        public User User { get; set; } = null!;
    }
}
