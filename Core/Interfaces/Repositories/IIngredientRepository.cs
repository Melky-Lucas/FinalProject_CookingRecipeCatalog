using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        Task<bool> HasNameAsync(string name);
        Task<bool> HasImageURLAsync(string url);
    }
}
