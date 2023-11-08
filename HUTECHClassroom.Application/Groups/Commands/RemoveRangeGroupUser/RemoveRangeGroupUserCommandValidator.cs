using HUTECHClassroom.Application.Common.Validators.Groups;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveRangeGroupUser;

public sealed class RemoveRangeGroupUserCommandValidator : AbstractValidator<RemoveRangeGroupUserCommand>
{
    public RemoveRangeGroupUserCommandValidator(GroupExistenceByNotNullIdValidator groupIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.GroupId).NotEmpty().NotNull()
            .SetValidator(groupIdValidator);
        RuleForEach(x => x.UserIds).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
