using Core.Interfaces.Repositories;
using Core.Models;
using Data.Context;
using Data.Repositories.Generic;

namespace Data.Repositories
{
    public class MeasureUnitRepository : GenericRepository<MeasureUnit>, IMeasureUnitRepository
    {
        public MeasureUnitRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
