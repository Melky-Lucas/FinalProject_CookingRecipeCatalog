using Core.Interfaces.Repositories;
using Core.Models;
using Data.Context;
using Data.Repositories.Generic;

namespace Data.Repositories
{
    public class Recipe_IngredientRepository : GenericRepository<Recipe_Ingredient>, IRecipe_IngredientRepository
    {
        public Recipe_IngredientRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
