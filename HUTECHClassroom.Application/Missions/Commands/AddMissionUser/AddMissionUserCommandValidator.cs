using HUTECHClassroom.Application.Common.Validators.Missions;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Missions.Commands.AddMissionUser;

public class AddMissionUserCommandValidator : AbstractValidator<AddMissionUserCommand>
{
    public AddMissionUserCommandValidator(MissionExistenceByNotNullIdValidator missionIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.MissionId).NotEmpty().NotNull()
            .SetValidator(missionIdValidator);
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
