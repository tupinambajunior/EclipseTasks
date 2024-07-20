using System.Collections.Generic;

namespace EclipseTasks.Core.Repository
{
    public interface ITaskRepository
    {
        void Create(Entities.Task task);

        void Update(Entities.Task task);

        void Delete(Entities.Task task);

        Task<IEnumerable<Entities.Task>> ListAsync(Guid projectId);

        Task<IEnumerable<Entities.Task>> ListAsync(DateTime start, DateTime end);

        Task<Entities.Task?> GetByIdAsync(Guid id);
    }
}