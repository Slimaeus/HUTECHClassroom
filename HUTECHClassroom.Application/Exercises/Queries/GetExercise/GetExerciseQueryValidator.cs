namespace HUTECHClassroom.Application.Exercises.Queries.GetExercise;

public sealed class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
{
    public GetExerciseQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
