using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Groups.Commands.UpdateGroup;

public sealed class UpdateGroupCommandValidator : UpdateCommandValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(GroupConstants.NAME_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(GroupConstants.DESCRIPTION_MAX_LENGTH);
    }
}
