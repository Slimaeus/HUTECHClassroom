namespace HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;

public class AddClassroomUserCommandValidator : AbstractValidator<AddClassroomUserCommand>
{
    public AddClassroomUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}
