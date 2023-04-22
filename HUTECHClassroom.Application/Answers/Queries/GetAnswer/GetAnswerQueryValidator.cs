namespace HUTECHClassroom.Application.Answers.Queries.GetAnswer;

public class GetAnswerQueryValidator : AbstractValidator<GetAnswerQuery>
{
    public GetAnswerQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
