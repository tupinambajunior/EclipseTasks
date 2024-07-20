using EclipseTasks.Core.Entities;

namespace EclipseTasks.Core.Repository
{
    public interface IHistoryTaskRepository
    {
        void Create(HistoryTask task);

        void Delete(HistoryTask task);

        Task<IEnumerable<HistoryTask>> ListAsync(Core.Entities.Task task);

    }
}