using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>,  IRecipeRepository
    {
        public RecipeRepository(RecipeCatalogDBContext context)
            : base(context)
        {
            
        }

        public async Task<bool> HasImageURLAsync(string url)
        {
            return await _table.AnyAsync(r => r.ImageUrl == url);
        }

        public async Task<bool> HasTitleAsync(string title)
        {
            return await _table.AnyAsync(r => r.Title == title);
        }
    }
}
