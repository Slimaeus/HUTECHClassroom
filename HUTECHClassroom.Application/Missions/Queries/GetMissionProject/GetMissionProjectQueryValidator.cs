namespace HUTECHClassroom.Application.Missions.Queries.GetMissionProject;

public class GetMissionProjectQueryValidator : AbstractValidator<GetMissionProjectQuery>
{
    public GetMissionProjectQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
