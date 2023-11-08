using HUTECHClassroom.Application.Common.Validators.Exercises;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Exercises.Commands.AddExerciseUser;

public sealed class AddExerciseUserCommandValidator : AbstractValidator<AddExerciseUserCommand>
{
    public AddExerciseUserCommandValidator(ExerciseExistenceByNotNullIdValidator exerciseIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.ExerciseId).NotEmpty().NotNull()
            .SetValidator(exerciseIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
