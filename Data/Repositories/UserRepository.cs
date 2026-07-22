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

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _table
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> HasEmailAsync(string email)
        {
            return await _table.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> Exists(int id)
        {
            return await _table.AnyAsync(u => u.Id == id);
        }
    }
}
