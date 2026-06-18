using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>,  IRecipeRepository
    {
        public RecipeRepository(RecipeCatalogDBContext context)
            : base(context)
        {
            
        }
    }
}
