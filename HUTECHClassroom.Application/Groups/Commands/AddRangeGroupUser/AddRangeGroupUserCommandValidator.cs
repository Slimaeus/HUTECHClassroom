using HUTECHClassroom.Application.Common.Validators.Groups;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Groups.Commands.AddRangeGroupUser;

public class AddRangeGroupUserCommandValidator : AbstractValidator<AddRangeGroupUserCommand>
{
    public AddRangeGroupUserCommandValidator(GroupExistenceByNotNullIdValidator groupIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.GroupId).NotEmpty().NotNull()
            .SetValidator(groupIdValidator);
        RuleForEach(x => x.UserIds).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
