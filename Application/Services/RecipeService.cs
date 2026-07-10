using Core.Interfaces.Repositories;
using Core.Models;

namespace Application.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            return await _recipeRepository.GetAllAsync();
        }

        public async Task<Recipe> GetById(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id) ??
                throw new InvalidOperationException("Recipe not found");

            return recipe;
        }

        public async Task Add(Recipe recipe)
        {
            _recipeRepository.Add(recipe);
        }

        public async Task Update(Recipe updatedRecipe)
        {
            _recipeRepository.Update(updatedRecipe);
        }

        public async Task Delete(int id)
        {
            var recipe = await GetById(id);

            _recipeRepository.Delete(recipe);
        }
    }
}