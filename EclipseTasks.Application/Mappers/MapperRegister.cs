using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EclipseTasks.Application.Mappers
{
    public static class MapperRegister
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<ProjectProfileMapper>();
            });

            services.AddSingleton<IMapper>(configuration.CreateMapper());

            return services;
        }
    }
}