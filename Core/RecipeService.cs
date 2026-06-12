using Core.Interfaces;
using Core.Model;

namespace Core
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public List<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAll();
        }

        public Recipe GetRecipeById(int id)
        {
            return _recipeRepository.GetById(id);
        }

        public void AddRecipe(Recipe recipe)
        {
            _recipeRepository.Add(recipe);
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            _recipeRepository.Update(updatedRecipe);
        }

        public void DeleteRecipe(int id)
        {
            _recipeRepository.Delete(id);
        }
    }
}
