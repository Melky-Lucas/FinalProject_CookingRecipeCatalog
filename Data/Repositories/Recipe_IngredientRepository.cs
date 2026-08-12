using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories
{
    public class Recipe_IngredientRepository : GenericRepository<Recipe_Ingredient>, IRecipe_IngredientRepository
    {
        public Recipe_IngredientRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
