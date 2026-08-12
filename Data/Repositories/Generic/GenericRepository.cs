using Core.Base;
using Core.Interfaces.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Generic
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _table;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _table.Add(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false)
        {
            return trackChanges ? 
                await _table.ToListAsync() : 
                await _table.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, bool trackChanges = true)
        {
            return trackChanges ?
                await _table.FindAsync(id) :
                await _table.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _table.AnyAsync(e => e.Id == id);
        }
    }
}
