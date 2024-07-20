using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        public DeleteTaskCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}