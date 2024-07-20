using EclipseTasks.Core.Entities;

namespace EclipseTasks.Core.Repository
{
    public interface IProjectRepository
    {
        void Create(Project project);
        void Delete(Project project);
        Task<Project?> GetByIdAsync(Guid projectId);
        Task<IEnumerable<ProjectNameDTO>> ListAsync();
    }
}