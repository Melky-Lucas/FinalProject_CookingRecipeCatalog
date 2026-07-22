using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IIngredientCategoryRepository : IGenericRepository<IngredientCategory>
    {
        Task<bool> HasNameAsync(string name);
    }
}