using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : UpdateCommandValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(100);
    }
}
