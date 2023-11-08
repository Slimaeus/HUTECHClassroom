namespace HUTECHClassroom.Application.Projects.Commands.AddMission;

public sealed class AddMissionCommandValidator : AbstractValidator<AddMissionCommand>
{
    public AddMissionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.MissionId).NotEmpty().NotNull();
    }
}
