namespace HUTECHClassroom.Application.Missions.Commands.CreateMission;

public class CreateMissionCommandValidator : AbstractValidator<CreateMissionCommand>
{
    public CreateMissionCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(100);

        RuleFor(x => x.ProjectId).NotEmpty().NotNull();
    }
}
