using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }

        public async Task<bool> HasImageURLAsync(string url)
        {
            return await _table.AnyAsync(i  => i.ImageUrl == url);
        }

        public async Task<bool> HasNameAsync(string name)
        {
            return await _table.AnyAsync(i => i.Name == name);
        }
    }
}
