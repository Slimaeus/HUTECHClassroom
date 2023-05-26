using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Missions.Commands.CreateMission;

public class CreateMissionCommandValidator : AbstractValidator<CreateMissionCommand>
{
    public CreateMissionCommandValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(MissionConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(MissionConstants.DESCRIPTION_MAX_LENGTH);

        RuleFor(x => x.ProjectId).NotEmpty().NotNull();
    }
}
