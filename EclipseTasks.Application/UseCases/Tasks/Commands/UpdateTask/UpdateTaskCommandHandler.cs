using EclipseTasks.Application.Exceptions;
using EclipseTasks.Application.Repositories;
using EclipseTasks.Application.Security;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using EclipseTasks.Core.VOs;
using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.UpdateTask
{
    public class UdpateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly IHistoryTaskRepository _historyTaskRepository;
        private readonly IAuthentication _authentication;

        public UdpateTaskCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository, IHistoryTaskRepository historyTaskRepository, IAuthentication authentication)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _historyTaskRepository = historyTaskRepository;
            _authentication = authentication;
        }

        public async System.Threading.Tasks.Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null) throw new NotFoundException("Task not found");
            
            var oldTask = (Core.Entities.Task)task.Clone();

            task.Title = request.Title;
            task.Deadline = request.Deadline;
            task.Description = request.Description;
            task.Status = request.Status;

            _taskRepository.Update(task);
            
            var changes = new HistoryTaskChanges(oldTask, task);
            var history = new HistoryTask
            {
                Id = Guid.NewGuid(),
                Actor = _authentication.GetUser(),
                GeneratedAt = DateTime.Now,
                Task = task,
                Content = changes.ToString()
            };

            _historyTaskRepository.Create(history);

            await _uow.SaveChangesAsync();
        }
    }
}