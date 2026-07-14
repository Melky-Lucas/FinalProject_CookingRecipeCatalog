using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>,  IRecipeRepository
    {
        public RecipeRepository(RecipeCatalogDBContext context)
            : base(context)
        {
            
        }
    }
}
