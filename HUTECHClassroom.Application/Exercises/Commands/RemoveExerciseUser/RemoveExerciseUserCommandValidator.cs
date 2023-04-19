using FluentValidation;

namespace HUTECHClassroom.Application.Exercises.Commands.RemoveExerciseUser;

public class RemoveExerciseUserCommandValidator : AbstractValidator<RemoveExerciseUserCommand>
{
    public RemoveExerciseUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.UserName).NotEmpty().NotNull();
    }
}
