using Core.Models;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();
        public Category GetById(int id);
        public void Add(Category category);
        public void Update(Category category);
        public void Delete(int id);
    }
}
