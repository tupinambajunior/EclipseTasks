using FluentValidation;

namespace EclipseTasks.Application.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
        }
    }
}