using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories
{
    public class MeasureUnitRepository : GenericRepository<MeasureUnit>, IMeasureUnitRepository
    {
        public MeasureUnitRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
