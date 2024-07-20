using EclipseTasks.Core.Repository;
using FluentValidation;

namespace EclipseTasks.Application.UseCases.Projects.Commands.DeleteProject
{
    public class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectValidator(ITaskRepository taskRepository)
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty).WithMessage("the project id must be valid")
                .MustAsync(async (projectId, cancellation) => {
                    var tasks = await taskRepository.ListAsync(projectId);
                    return !tasks.Any(t=>t.Status != Core.Enums.StatusTask.Done);
                }).WithMessage("isn't possible to delete the project with pending tasks. Please, finish the tasks before");
        }
    }
}