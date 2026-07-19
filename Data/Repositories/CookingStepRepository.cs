using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories
{
    public class CookingStepRepository : GenericRepository<CookingStep>, ICookingStepRepository
    {
        public CookingStepRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
