using EclipseTasks.Core.Entities;

namespace EclipseTasks.Application.Security
{
    public interface IAuthentication
    {
        public User GetUser();
    }
}