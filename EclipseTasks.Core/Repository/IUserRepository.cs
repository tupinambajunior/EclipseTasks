using EclipseTasks.Core.Entities;

namespace EclipseTasks.Core.Repository
{
    public interface IUserRepository
    {
        public Task<User?> GetByKeyAsync(string key);
    }
}