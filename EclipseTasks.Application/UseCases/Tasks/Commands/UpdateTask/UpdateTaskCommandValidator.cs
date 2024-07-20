using FluentValidation;

namespace EclipseTasks.Application.UseCases.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(s => s.Title).NotEmpty();
            RuleFor(s => s.Status).IsInEnum();
        }
    }
}