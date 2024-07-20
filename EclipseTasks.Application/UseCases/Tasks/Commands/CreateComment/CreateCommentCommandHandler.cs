using EclipseTasks.Application.Repositories;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using MediatR;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICommentTaskRepository _commentTaskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public CreateCommentCommandHandler(IUnitOfWork uow, ICommentTaskRepository commentTaskRepository, IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _uow = uow;
            _commentTaskRepository = commentTaskRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new CommentTask()
            {
                Id = Guid.NewGuid(),
                Actor = await _userRepository.GetByKeyAsync(request.UserKey),
                CreatedAt = DateTime.Now,
                Task = await _taskRepository.GetByIdAsync(request.TaskId),
                Comment = request.Comment,
            };

            _commentTaskRepository.Create(comment);
            await _uow.SaveChangesAsync();
        }
    }
}