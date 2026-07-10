using Core.Interfaces.Repositories;
using Core.Models;
using Data.Context;
using Data.Repositories.Generic;

namespace Data.Repositories
{
    public class TipRepository : GenericRepository<Tip>, ITipRepository
    {
        public TipRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
