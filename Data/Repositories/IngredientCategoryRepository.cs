using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories
{
    public class IngredientCategoryRepository : GenericRepository<IngredientCategory>, IIngredientCategoryRepository
    {
        public IngredientCategoryRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
