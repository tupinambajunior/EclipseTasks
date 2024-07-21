using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using EclipseTasks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class HistoryTaskRepository : GenericRepository<HistoryTask>, IHistoryTaskRepository
    {
        public HistoryTaskRepository(AppDbContext context) : base(context)
        {
                
        }

        public async Task<IEnumerable<HistoryTask>> ListAsync(Core.Entities.Task task)
        {
            return await _context.Histories.Where(h => h.Task.Id.Equals(task.Id)).ToListAsync();
        }
    }
}