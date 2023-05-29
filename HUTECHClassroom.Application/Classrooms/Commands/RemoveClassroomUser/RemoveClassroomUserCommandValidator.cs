namespace HUTECHClassroom.Application.Classrooms.Commands.RemoveClassroomUser;

public class RemoveClassroomUserCommandValidator : AbstractValidator<RemoveClassroomUserCommand>
{
    public RemoveClassroomUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}
