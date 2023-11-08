namespace HUTECHClassroom.Application.Majors.Queries.GetMajor;

public sealed class GetMajorQueryValidator : AbstractValidator<GetMajorQuery>
{
    public GetMajorQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
