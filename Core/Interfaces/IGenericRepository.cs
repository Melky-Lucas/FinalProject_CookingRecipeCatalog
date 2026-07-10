using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false);
        Task<T?> GetByIdAsync(object id);
        void AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
