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

        public async Task<IEnumerable<Recipe>> GetAllByQueryAsync(
            string? title,
            int? userId,
            int[]? categoryIds,
            int[]? requiredIngredientIds,
            int[]? optionalIngredientIds,
            int[]? excludedIngredientIds,
            int pageSize = 10,
            int pageNumber = 1)
        {
            IQueryable<Recipe> query = _table;

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(r => r.Title.StartsWith(title));
            }

            if (userId.HasValue)
            {
                query = query.Where(r => r.UserId == userId.Value);
            }

            if (categoryIds != null && categoryIds.Length > 0)
            {
                query = query.Where(r => r.Recipe_Categories.Any(rc => categoryIds.Contains(rc.CategoryId)));
            }

            if (requiredIngredientIds != null && requiredIngredientIds.Length > 0)
            {
                query = query.Where(r => r.Recipe_Ingredients
                    .Count(ri => requiredIngredientIds.Contains(ri.IngredientId)) == requiredIngredientIds.Length);
            }

            if (optionalIngredientIds != null && optionalIngredientIds.Length > 0)
            {
                query = query.Where(r => r.Recipe_Ingredients
                    .Any(ri => optionalIngredientIds.Contains(ri.IngredientId)));
            }

            if (excludedIngredientIds != null && excludedIngredientIds.Length > 0)
            {
                query = query.Where(r => !r.Recipe_Ingredients
                    .Any(ri => excludedIngredientIds.Contains(ri.IngredientId)));
            }

            return await query
                .OrderBy(r => r.Title)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
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
