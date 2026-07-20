using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }

        public async Task<User?> GetByEmailWithRoleAsync(string email)
        {
            return await _table
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> HasEmailAsync(string email)
        {
            return await _table.AnyAsync(u => u.Email == email);
        }
    }
}
