using HUTECHClassroom.Application.Common.Validators.Groups;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator(GroupExistenceByNotNullIdValidator groupIdValidator)
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(ProjectConstants.NAME_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(ProjectConstants.DESCRIPTION_MAX_LENGTH);

        RuleFor(x => x.GroupId).NotNull().NotEmpty()
            .SetValidator(groupIdValidator);
    }
}
