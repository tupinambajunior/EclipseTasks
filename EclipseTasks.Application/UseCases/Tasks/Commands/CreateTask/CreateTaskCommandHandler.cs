using EclipseTasks.Application.Repositories;
using EclipseTasks.Application.Security;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using EclipseTasks.Core.VOs;
using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IAuthentication _authentication;
        private readonly IHistoryTaskRepository _historyTaskRepository;
        private readonly IUserRepository _userRepository;

        public CreateTaskCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository, IProjectRepository projectRepository, IAuthentication authentication, IHistoryTaskRepository historyTaskRepository, IUserRepository userRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _authentication = authentication;
            _historyTaskRepository = historyTaskRepository;
            _userRepository = userRepository;
        }

        public async System.Threading.Tasks.Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);

            var user = await _userRepository.GetByKeyAsync(request.ActorKey);

            var task = new Core.Entities.Task()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Actor = user,
                Title = request.Title,
                Deadline = request.Deadline,
                Description = request.Description,
                Priority = request.Priority,
                Status = request.Status,
                Project = project
            };

            _taskRepository.Create(task);
            
            var changes = new HistoryTaskChanges(null, task);
            var history = new HistoryTask
            {
                Id = Guid.NewGuid(),
                Actor = user,
                GeneratedAt = DateTime.Now,
                Task = task,
                Content = changes.ToString()
            };

            _historyTaskRepository.Create(history);

            await _uow.SaveChangesAsync();
        }
    }
}