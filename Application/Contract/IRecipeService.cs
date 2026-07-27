using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IRecipeService : IBaseService<RecipeDTO, CreateRecipeDTO, UpdateRecipeDTO>
    {
        Task<ServiceResult> UpdateRecipeStepsAsync(int recipeId, ICollection<UpdateRecipeStepDTO> dto);
        Task<ServiceResult> AddRecipeCategoryAsync(int recipeId, int categoryId);
        Task<ServiceResult> RemoveRecipeCategoryAsync(int recipeId, int categoryId);
        Task<ServiceResult> UpdateRecipe_IngredientAsync(int recipeId, UpdateRecipe_IngredientDTO dto);
        Task<ServiceResult> AddRecipe_IngredientAsync(int recipeId, CreateRecipe_IngredientDTO dto);
        Task<ServiceResult> RemoveRecipe_IngredientAsync(int recipeId, int recipe_ingredientId);
    }
}
