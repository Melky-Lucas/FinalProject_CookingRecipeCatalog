using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories
{
    public class Recipe_CategoryRepository : GenericRepository<Recipe_Category>, IRecipe_CategoryRepository
    {
        public Recipe_CategoryRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
