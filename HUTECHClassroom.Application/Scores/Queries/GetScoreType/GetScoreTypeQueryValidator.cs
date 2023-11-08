namespace HUTECHClassroom.Application.ScoreTypes.Queries.GetScoreType;

public sealed class GetScoreTypeQueryValidator : AbstractValidator<GetScoreTypeQuery>
{
    public GetScoreTypeQueryValidator()
        => RuleFor(x => x.Id).NotEmpty().NotNull();
}
