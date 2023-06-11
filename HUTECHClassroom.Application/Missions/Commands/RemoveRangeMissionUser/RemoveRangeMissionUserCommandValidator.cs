using HUTECHClassroom.Application.Common.Validators.Missions;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Missions.Commands.RemoveRangeMissionUser;

public class RemoveRangeMissionUserCommandValidator : AbstractValidator<RemoveRangeMissionUserCommand>
{
    public RemoveRangeMissionUserCommandValidator(MissionExistenceByNotNullIdValidator missionIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.MissionId).NotEmpty().NotNull()
            .SetValidator(missionIdValidator);
        RuleForEach(x => x.UserIds).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
