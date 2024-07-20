using MediatR;

namespace EclipseTasks.Application.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest
    {
        public string Name { get; set; }
    }
}