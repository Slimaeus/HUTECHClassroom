using HUTECHClassroom.Application.Common.Validators.Missions;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser;

public class RemoveMissionUserCommandValidator : AbstractValidator<RemoveMissionUserCommand>
{
    public RemoveMissionUserCommandValidator(MissionExistenceByNotNullIdValidator missionIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.MissionId).NotEmpty().NotNull()
            .SetValidator(missionIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
