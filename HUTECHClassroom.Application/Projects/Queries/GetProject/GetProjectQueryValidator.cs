namespace HUTECHClassroom.Application.Projects.Queries.GetProject;

public sealed class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
{
    public GetProjectQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
