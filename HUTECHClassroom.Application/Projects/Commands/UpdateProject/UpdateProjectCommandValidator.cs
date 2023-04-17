using FluentValidation;
using HUTECHClassroom.Application.Projects.Commands.UpdateProject;

namespace HUTECHClassroom.Application.Projects.Commands
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
