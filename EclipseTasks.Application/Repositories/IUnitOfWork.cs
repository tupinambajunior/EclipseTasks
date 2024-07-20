namespace EclipseTasks.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}