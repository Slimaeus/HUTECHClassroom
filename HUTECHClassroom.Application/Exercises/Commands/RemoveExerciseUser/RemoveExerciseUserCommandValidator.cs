using HUTECHClassroom.Application.Common.Validators.Exercises;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Exercises.Commands.RemoveExerciseUser;

public sealed class RemoveExerciseUserCommandValidator : AbstractValidator<RemoveExerciseUserCommand>
{
    public RemoveExerciseUserCommandValidator(ExerciseExistenceByNotNullIdValidator exerciseIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.ExerciseId).NotEmpty().NotNull()
            .SetValidator(exerciseIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
