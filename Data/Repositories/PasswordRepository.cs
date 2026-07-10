using Core.Interfaces.Repositories;
using Core.Models;
using Data.Context;
using Data.Repositories.Generic;

namespace Data.Repositories
{
    public class PasswordRepository : GenericRepository<Password>, IPasswordRepository
    {
        public PasswordRepository(RecipeCatalogDBContext context)
            : base(context)
        {
        }
    }
}
