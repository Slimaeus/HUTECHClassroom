using FluentValidation;

namespace HUTECHClassroom.Application.Projects.Commands.AddMission;

public class AddMissionCommandValidator : AbstractValidator<AddMissionCommand>
{
    public AddMissionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.MissionId).NotEmpty().NotNull();
    }
}
