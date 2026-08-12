using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CookingStepRepository : GenericRepository<CookingStep>, ICookingStepRepository
    {
        public CookingStepRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<CookingStep>> GetStepsByRecipeIdAsync(int recipeId, bool trackChanges = false)
        {
            return trackChanges ?
                await _table.Where(cs => cs.RecipeId == recipeId).ToListAsync() :
                await _table.Where(cs => cs.RecipeId == recipeId).AsNoTracking().ToListAsync();
        }

        public void RemoveRange(IEnumerable<CookingStep> steps)
        {
            _table.RemoveRange(steps);
        }
    }
}
