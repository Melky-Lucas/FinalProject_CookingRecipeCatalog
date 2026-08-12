using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _table.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
