using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.Users;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Groups.Commands.CreateGroup;

public sealed class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator(UserExistenceByNotNullIdValidator userIdValidator, ClassroomExistenceByNotNullIdValidator classroomValidator)
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(GroupConstants.NAME_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(GroupConstants.DESCRIPTION_MAX_LENGTH);

        RuleFor(x => x.LeaderId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull()
            .SetValidator(classroomValidator);
    }
}
