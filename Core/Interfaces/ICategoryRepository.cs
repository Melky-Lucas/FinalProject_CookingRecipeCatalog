using Core.Models;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        public List<RecipeCategory> GetAll();
        public RecipeCategory GetById(int id);
        public void Add(RecipeCategory category);
        public void Update(RecipeCategory category);
        public void Delete(int id);
    }
}
