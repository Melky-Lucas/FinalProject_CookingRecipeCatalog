using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class InMemory_CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = [];

        public InMemory_CategoryRepository()
        {
            _categories.Add(new Category { Id = 1, Name = "Breakfast", Description = "Good meals to start the day" });
            _categories.Add(new Category { Id = 2, Name = "Lunch", Description = "Main meal of the day, good to recover energy"});
        }
        public List<Category> GetAll()
        {
            return _categories;
        }

        public Category GetById(int id)
        {
            return _categories.Find(c => c.Id == id) ?? throw new InvalidOperationException($"The category with this ID({id}) doesn't exist");
        }

        public void Add(Category category)
        {
            category.Id = _categories.Count + 1;
            _categories.Add(category);
        }
        public void Update(Category category)
        {
            var existingCategory = GetById(category.Id);
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
        }

        public void Delete(int id)
        {
            _categories.Remove(GetById(id));
        }
    }
}
