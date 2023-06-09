using HUTECHClassroom.Application.Common.Validators.Groups;
using HUTECHClassroom.Application.Common.Validators.Users;
using HUTECHClassroom.Application.Groups.Commands.AddGroupUser;

namespace HUTECHClassroom.Application.Groups.Commands.AddGroupUsers;

public class AddGroupUsersCommandValidator : AbstractValidator<AddGroupUserCommand>
{
    public AddGroupUsersCommandValidator(GroupExistenceByNotNullIdValidator groupIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.GroupId).NotEmpty().NotNull()
            .SetValidator(groupIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
