namespace HUTECHClassroom.Application.Exercises.Commands.AddExerciseUser;

public class AddExerciseUserCommandValidator : AbstractValidator<AddExerciseUserCommand>
{
    public AddExerciseUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}
