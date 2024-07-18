using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PgContext _dbContext;
        private DbSet<T> entities;

        public Repository(PgContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<T>();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await entities.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<string> CreateAsync(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entities.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);

            entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
