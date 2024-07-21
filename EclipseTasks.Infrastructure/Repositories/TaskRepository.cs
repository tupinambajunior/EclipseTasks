using EclipseTasks.Core.Repository;
using EclipseTasks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class TaskRepository : GenericRepository<Core.Entities.Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Core.Entities.Task>> ListAsync(Guid projectId)
        {
            return await _context.Tasks.Where(t => t.Project.Id.Equals(projectId)).ToListAsync();
        }

        public async Task<IEnumerable<Core.Entities.Task>> ListAsync(DateTime start, DateTime end)
        {
            return await _context.Tasks
                .Include(t=>t.Actor)
                .Where(t => t.CreatedAt >= start && t.CreatedAt <= end)
                .ToListAsync();
        }
    }
}