using EclipseTasks.Core.Entities;
using MediatR;

namespace EclipseTasks.Application.UseCases.Projects.Queries.ListProjects
{
    public class ListProjectsQuery : IRequest<IEnumerable<ProjectNameDTO>>
    {
    }
}