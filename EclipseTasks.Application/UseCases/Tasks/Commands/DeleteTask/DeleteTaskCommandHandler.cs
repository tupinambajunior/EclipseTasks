using EclipseTasks.Application.Exceptions;
using EclipseTasks.Application.Repositories;
using EclipseTasks.Core.Repository;
using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly IHistoryTaskRepository _historyTaskRepository;

        public DeleteTaskCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository, IHistoryTaskRepository historyTaskRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _historyTaskRepository = historyTaskRepository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null) throw new NotFoundException("Task not found");

            var histories = await _historyTaskRepository.ListAsync(task);
            foreach ( var history in histories)
                _historyTaskRepository.Delete(history);
            
            _taskRepository.Delete(task);
            await _uow.SaveChangesAsync();
        }
    }
}