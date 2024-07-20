using EclipseTasks.Application.Repositories;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using MediatR;

namespace EclipseTasks.Application.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _uow;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork uow)
        {
            _projectRepository = projectRepository;
            _uow = uow;
        }

        public async System.Threading.Tasks.Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project()
            {
                Name = request.Name,
            };

            _projectRepository.Create(project);
            await _uow.SaveChangesAsync();
        }
    }
}