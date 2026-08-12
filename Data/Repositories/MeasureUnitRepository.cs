using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MeasureUnitRepository : GenericRepository<MeasureUnit>, IMeasureUnitRepository
    {
        public MeasureUnitRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }

        public async Task<bool> HasAbbAsync(string abb)
        {
            return await _table.AnyAsync(a => a.Abbreviation == abb);
        }

        public async Task<bool> HasNameAsync(string name)
        {
            return await _table.AnyAsync(a => a.Name == name);
        }

        public async Task AddRangeAsync(IEnumerable<MeasureUnit> units)
        {
            await _table.AddRangeAsync(units);
        }
    }
}
