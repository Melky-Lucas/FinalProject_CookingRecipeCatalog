using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class IngredientCategoryRepository : GenericRepository<IngredientCategory>, IIngredientCategoryRepository
    {
        public IngredientCategoryRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }

        public async Task<bool> HasNameAsync(string name)
        {
             return await _table.AnyAsync(c => c.Name == name);
        }
    }
}
