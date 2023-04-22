namespace HUTECHClassroom.Application.Projects.Queries.GetProjectMission;

public class GetProjectMissionQueryValidator : AbstractValidator<GetProjectMissionQuery>
{
    public GetProjectMissionQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
