using Core.Interfaces.Repositories;
using Core.Models;
using Data.Context;
using Data.Repositories.Generic;

namespace Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
