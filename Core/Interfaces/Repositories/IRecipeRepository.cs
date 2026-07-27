using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<bool> HasTitleAsync(string title);
        Task<bool> HasImageURLAsync(string url);
    }
}
