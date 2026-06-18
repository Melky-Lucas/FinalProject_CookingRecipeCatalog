using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return await _recipeRepository.GetAllAsync();
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id) ??
                throw new InvalidOperationException();

            return recipe;
        }

        public async Task AddRecipe(Recipe recipe)
        {
            await _recipeRepository.AddAsync(recipe);
        }

        public async Task UpdateRecipe(Recipe updatedRecipe)
        {
            await _recipeRepository.Update(updatedRecipe);
        }

        public async Task DeleteRecipe(int id)
        {
            var recipe = await GetRecipeById(id);

            await _recipeRepository.Delete(recipe);
        }
    }
}