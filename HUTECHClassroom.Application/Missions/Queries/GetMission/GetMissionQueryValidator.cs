using FluentValidation;

namespace HUTECHClassroom.Application.Missions.Queries.GetMission;

public class GetMissionQueryValidator : AbstractValidator<GetMissionQuery>
{
    public GetMissionQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
