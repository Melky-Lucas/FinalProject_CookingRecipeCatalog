using Core.Models;

namespace Core.Interfaces
{
    public interface IRecipeRepository
    {
        public List<Recipe> GetAll();
        public Recipe GetById(int id);
        public void Add(Recipe recipe);
        public void Update(Recipe updatedRecipe);
        public void Delete(int id);
    }
}
