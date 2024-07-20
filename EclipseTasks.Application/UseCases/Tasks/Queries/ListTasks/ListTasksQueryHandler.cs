using EclipseTasks.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Application.UseCases.Tasks.Queries.ListTasks
{
    public class ListTasksQueryHandler : IRequestHandler<ListTasksQuery, IEnumerable<Core.Entities.Task>>
    {
        private readonly ITaskRepository _taskRepository;

        public ListTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<Core.Entities.Task>> Handle(ListTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.ListAsync(request.ProjectId);
        }
    }
}
