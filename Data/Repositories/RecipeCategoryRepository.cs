using Core.Interfaces;
using Core.Models;

namespace Data.Repositories
{
    public class RecipeCategoryRepository : GenericRepository<RecipeCategory>, IRecipeCategoryRepository
    {
        public RecipeCategoryRepository(RecipeCatalogDBContext context)
            : base(context)
        {
            
        }
    }
}
