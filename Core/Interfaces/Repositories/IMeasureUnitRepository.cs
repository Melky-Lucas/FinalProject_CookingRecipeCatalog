using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IMeasureUnitRepository : IGenericRepository<MeasureUnit>
    {
        Task<bool> HasNameAsync(string name);
        Task<bool> HasAbbAsync(string abb);
        Task AddRangeAsync(IEnumerable<MeasureUnit> units);
    }
}