using MediatR;

namespace EclipseTasks.Application.UseCases.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest
    {
        public DeleteProjectCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}