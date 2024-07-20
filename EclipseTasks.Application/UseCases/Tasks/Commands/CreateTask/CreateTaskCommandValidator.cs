using EclipseTasks.Application.Configuration;
using EclipseTasks.Core.Repository;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator(ITaskRepository taskRepository, IOptions<AppOptions> options)
        {
            RuleFor(s => s.Title).NotEmpty();
            RuleFor(s => s.Priority).IsInEnum();
            RuleFor(s => s.Status).IsInEnum();
            RuleFor(s => s.ProjectId)
                .NotEmpty().WithMessage("ProjectId is Empty")
                .MustAsync(async (projectId, cancellation) =>
                {
                    var tasks = await taskRepository.ListAsync(projectId);
                    return tasks.Count() <= options.Value.MaxTaskInProject;
                }).WithMessage("Exceeded max tasks in the project");
        }
    }
}