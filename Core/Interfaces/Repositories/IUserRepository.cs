using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> HasEmailAsync(string email);
        Task<bool> Exists(int id);
    }
}

