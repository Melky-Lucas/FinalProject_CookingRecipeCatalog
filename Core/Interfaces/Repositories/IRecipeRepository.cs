using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetAllByQueryAsync(string? title, int? User_Id, int[]? Category_Ids,
            int[]? requiredIngredientIds, int[]? optionalIngredientIds, int[]? excludedIngredientIds,
            int Page_size = 10, int Page_number = 1);
        Task<bool> HasTitleAsync(string title);
        Task<bool> HasImageURLAsync(string url);
    }
}
