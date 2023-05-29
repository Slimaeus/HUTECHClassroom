using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;

public class AddClassroomUserCommandValidator : AbstractValidator<AddClassroomUserCommand>
{
    public AddClassroomUserCommandValidator(UserExistenceByNotNullIdValidator userIdValidator, ClassroomExistenceByNotNullIdValidator classroomIdValidator)
    {
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull()
            .SetValidator(classroomIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
