using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories
{
    public class TipRepository : GenericRepository<Tip>, ITipRepository
    {
        public TipRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
