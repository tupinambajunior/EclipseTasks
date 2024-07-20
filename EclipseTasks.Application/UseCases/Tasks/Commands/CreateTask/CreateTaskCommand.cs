using EclipseTasks.Core.Enums;
using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime Deadline { get; set; }
        public StatusTask Status { get; set; }
        public PriorityTask Priority { get; set; }
        public string ActorKey { get; set; }
    }
}