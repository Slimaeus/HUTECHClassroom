using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(GroupConstants.NAME_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(GroupConstants.DESCRIPTION_MAX_LENGTH);

        RuleFor(x => x.LeaderName).NotEmpty().NotNull();
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull();
    }
}
