using FluentValidation;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionUser;

public class GetMissionUserQueryValidator : AbstractValidator<GetMissionUserQuery>
{
    public GetMissionUserQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
