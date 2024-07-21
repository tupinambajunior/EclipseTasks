using EclipseTasks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual void Create(TEntity entity)
        {
            _context.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
    }
}