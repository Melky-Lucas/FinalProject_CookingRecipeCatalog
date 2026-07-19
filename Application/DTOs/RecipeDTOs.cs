using static Core.Enums.ModelEnums;

namespace Application.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int Servings { get; set; }
        public RecipeDifficulty Difficulty { get; set; }
        public int Calories { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<Recipe_IngredientDTO> Recipe_Ingredients { get; set; } = [];
        public ICollection<RecipeCategoryDTO> Categories { get; set; } = [];
        public ICollection<RecipeCookingStepDTO> CookingSteps { get; set; } = [];
        public ICollection<RecipeTipDTO> Tips { get; set; } = [];
        public string Username { get; set; } = null!;
    }

    public class CreateRecipeDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int Servings { get; set; }
        public RecipeDifficulty Difficulty { get; set; }
        public int Calories { get; set; }
        public int UserId { get; set; }
        public bool IsPublic { get; set; }
        public int[] Category_Ids { get; set; } = [];
        public ICollection<CreateRecipe_IngredientDTO> Recipe_Ingredients { get; set; } = [];
        public ICollection<CreateRecipeCookingStepDTO> CookingSteps { get; set; } = [];
        public ICollection<CreateRecipeTipDTO> Tips { get; set; } = [];
    }

    public class UpdateRecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int Servings { get; set; }
        public RecipeDifficulty Difficulty { get; set; }
        public int Calories { get; set; }
        public bool IsPublic { get; set; }
        public int[] Category_Ids { get; set; } = [];
        public ICollection<UpdateRecipe_IngredientDTO> Recipe_Ingredients { get; set; } = [];
    }
}
