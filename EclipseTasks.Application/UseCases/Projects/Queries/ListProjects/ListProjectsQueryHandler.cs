using AutoMapper;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using MediatR;

namespace EclipseTasks.Application.UseCases.Projects.Queries.ListProjects
{
    internal class ListProjectsQueryHandler : IRequestHandler<ListProjectsQuery, IEnumerable<ProjectNameDTO>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ListProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectNameDTO>> Handle(ListProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.ListAsync();
        }
    }
}