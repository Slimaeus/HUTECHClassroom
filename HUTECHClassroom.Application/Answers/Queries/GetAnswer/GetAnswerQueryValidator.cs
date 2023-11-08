namespace HUTECHClassroom.Application.Answers.Queries.GetAnswer;

public sealed class GetAnswerQueryValidator : AbstractValidator<GetAnswerQuery>
{
    public GetAnswerQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
