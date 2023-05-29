using HUTECHClassroom.Application.Common.Validators.Groups;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
internal class AddGroupLeaderCommandValidator : AbstractValidator<AddGroupLeaderCommand>
{
    public AddGroupLeaderCommandValidator(GroupExistenceByNotNullIdValidator groupIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.GroupId).NotNull().NotEmpty()
            .SetValidator(groupIdValidator);
        RuleFor(x => x.UserId).NotNull().NotEmpty()
            .SetValidator(userIdValidator);
    }
}
