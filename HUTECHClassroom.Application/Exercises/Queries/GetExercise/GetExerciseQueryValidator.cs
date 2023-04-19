using FluentValidation;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExercise;

public class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
{
    public GetExerciseQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
