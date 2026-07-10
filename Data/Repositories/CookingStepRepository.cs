using Core.Interfaces.Repositories;
using Core.Models;
using Data.Context;
using Data.Repositories.Generic;

namespace Data.Repositories
{
    public class CookingStepRepository : GenericRepository<CookingStep>, ICookingStepRepository
    {
        public CookingStepRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
