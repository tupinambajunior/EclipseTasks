using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using EclipseTasks.Infrastructure.Persistence;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByKeyAsync(string key)
        {
            return _context.Users.FirstOrDefault(u => u.Email.Equals(key));
        }
    }
}