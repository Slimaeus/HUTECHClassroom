using HUTECHClassroom.Application.Common.Validators.Groups;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Groups.Commands.AddGroupUser;

public sealed class AddGroupUserCommandValidator : AbstractValidator<AddGroupUserCommand>
{
    public AddGroupUserCommandValidator(GroupExistenceByNotNullIdValidator groupIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.GroupId).NotEmpty().NotNull()
            .SetValidator(groupIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
