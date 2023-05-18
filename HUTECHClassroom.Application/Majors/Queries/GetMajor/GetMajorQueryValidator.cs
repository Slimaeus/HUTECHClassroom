namespace HUTECHClassroom.Application.Majors.Queries.GetMajor;

public class GetMajorQueryValidator : AbstractValidator<GetMajorQuery>
{
    public GetMajorQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
