using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Classrooms.Commands.RemoveClassroomUser;

public sealed class RemoveClassroomUserCommandValidator : AbstractValidator<RemoveClassroomUserCommand>
{
    public RemoveClassroomUserCommandValidator(ClassroomExistenceByNotNullIdValidator classroomIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull()
            .SetValidator(classroomIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
