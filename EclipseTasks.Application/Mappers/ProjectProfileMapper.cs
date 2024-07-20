using AutoMapper;
using EclipseTasks.Core.Entities;

namespace EclipseTasks.Application.Mappers
{
    public class ProjectProfileMapper : Profile
    {
        public ProjectProfileMapper()
        {
            CreateMap<Project, ProjectNameDTO>();
        }
    }
}