using HUTECHClassroom.Application.Common.Validators.Groups;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveGroupLeader;

public sealed class RemoveGroupLeaderCommandValidator : AbstractValidator<RemoveGroupLeaderCommand>
{
    public RemoveGroupLeaderCommandValidator(GroupExistenceByNotNullIdValidator groupIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.GroupId).NotNull().NotEmpty()
            .SetValidator(groupIdValidator);
        RuleFor(x => x.UserId).NotNull().NotEmpty()
            .SetValidator(userIdValidator);
    }
}
