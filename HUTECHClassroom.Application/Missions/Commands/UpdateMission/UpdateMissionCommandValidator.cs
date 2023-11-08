using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Missions.Commands.UpdateMission;

public sealed class UpdateMissionCommandValidator : UpdateCommandValidator<UpdateMissionCommand>
{
    public UpdateMissionCommandValidator()
    {
        RuleFor(x => x.Title).MaximumLength(MissionConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(MissionConstants.DESCRIPTION_MAX_LENGTH);
    }
}
