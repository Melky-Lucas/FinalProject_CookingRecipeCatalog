using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false);
        Task<T?> GetByIdAsync(object id);

        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
