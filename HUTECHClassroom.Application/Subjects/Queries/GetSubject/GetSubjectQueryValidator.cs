namespace HUTECHClassroom.Application.Subjects.Queries.GetSubject;

public sealed class GetSubjectQueryValidator : AbstractValidator<GetSubjectQuery>
{
    public GetSubjectQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
