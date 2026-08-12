namespace Core.Interfaces.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false);
        Task<T?> GetByIdAsync(int id, bool trackChanges = true);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(int  id);
    }
}
