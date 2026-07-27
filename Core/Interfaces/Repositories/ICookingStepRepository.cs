using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface ICookingStepRepository : IGenericRepository<CookingStep>
    {
        Task<IEnumerable<CookingStep>> GetStepsByRecipeIdAsync(int recipeId, bool trackChanges = false);
        void RemoveRange(IEnumerable<CookingStep> steps);
    }
}
