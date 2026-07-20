using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailWithRoleAsync(string email);
        Task<bool> HasEmailAsync(string email);
    }
}

