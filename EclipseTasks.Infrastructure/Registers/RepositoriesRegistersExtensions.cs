using EclipseTasks.Application.Configuration;
using EclipseTasks.Application.Repositories;
using EclipseTasks.Core.Repository;
using EclipseTasks.Infrastructure.Persistence;
using EclipseTasks.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EclipseTasks.Infrastructure.Registers
{
    public static class RepositoriesRegistersExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, AppOptions options)
        {
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(options.ConnectionString));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICommentTaskRepository, CommentTaskRepository>();
            services.AddScoped<IHistoryTaskRepository, HistoryTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}