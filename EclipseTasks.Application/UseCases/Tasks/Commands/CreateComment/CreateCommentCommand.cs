using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        public Guid TaskId { get; set; }

        public string Comment { get; set; }

        public string UserKey { get; set; }
    }
}