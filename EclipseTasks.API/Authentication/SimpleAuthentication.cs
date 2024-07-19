using EclipseTasks.Application.Security;
using EclipseTasks.Core.Entities;

namespace EclipseTasks.Api.Authentication
{
    public class SimpleAuthentication : IAuthentication
    {
        private readonly IHttpContextAccessor accessor;

        public SimpleAuthentication(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public User GetUser()
        {
            var email = this.accessor.HttpContext.User.Claims.First(c => c.Type == "UserKey").Value;

            return new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                AccessMode = email.StartsWith("gestor")
                    ? Core.Enums.AccessMode.Administrator
                    : Core.Enums.AccessMode.Common
            };
        }
    }
}