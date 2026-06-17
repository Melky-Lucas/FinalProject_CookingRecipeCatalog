using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<RecipeCategory> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public RecipeCategory GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public void Addcategory(RecipeCategory category)
        {
            _categoryRepository.Add(category);
        }

        public void UpdateCategory(RecipeCategory category)
        {
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
        }
    }
}
