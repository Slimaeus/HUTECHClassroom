namespace HUTECHClassroom.Application.Projects.Queries.GetProject;

public class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
{
    public GetProjectQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
