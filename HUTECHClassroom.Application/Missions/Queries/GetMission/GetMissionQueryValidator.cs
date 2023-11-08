namespace HUTECHClassroom.Application.Missions.Queries.GetMission;

public sealed class GetMissionQueryValidator : AbstractValidator<GetMissionQuery>
{
    public GetMissionQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
