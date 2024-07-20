using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Queries.ListTasks
{
    public class ListTasksQuery : IRequest<IEnumerable<Core.Entities.Task>>
    {
        public ListTasksQuery(Guid projectId)
        {
            this.ProjectId = projectId;
        }

        public Guid ProjectId { get; set; }
    }
}