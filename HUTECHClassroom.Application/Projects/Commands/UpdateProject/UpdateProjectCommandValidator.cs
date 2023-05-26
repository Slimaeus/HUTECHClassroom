using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : UpdateCommandValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(ProjectConstants.NAME_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(ProjectConstants.DESCRIPTION_MAX_LENGTH);
    }
}
