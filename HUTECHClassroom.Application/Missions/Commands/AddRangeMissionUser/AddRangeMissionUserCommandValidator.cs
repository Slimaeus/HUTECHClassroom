using HUTECHClassroom.Application.Common.Validators.Missions;
using HUTECHClassroom.Application.Common.Validators.Users;

namespace HUTECHClassroom.Application.Missions.Commands.AddRangeMissionUser;

public sealed class AddRangeMissionUserCommandValidator : AbstractValidator<AddRangeMissionUserCommand>
{
    public AddRangeMissionUserCommandValidator(MissionExistenceByNotNullIdValidator missionIdValidator, UserExistenceByNotNullIdValidator userIdValidator)
    {
        RuleFor(x => x.MissionId).NotEmpty().NotNull()
            .SetValidator(missionIdValidator);
        RuleForEach(x => x.UserIds).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
    }
}
