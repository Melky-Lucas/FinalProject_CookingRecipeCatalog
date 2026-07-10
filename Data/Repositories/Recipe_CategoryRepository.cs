using Core.Interfaces.Repositories;
using Core.Models;
using Data.Context;
using Data.Repositories.Generic;

namespace Data.Repositories
{
    public class Recipe_CategoryRepository : GenericRepository<Recipe_Category>, IRecipe_CategoryRepository
    {
        public Recipe_CategoryRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
