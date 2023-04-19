using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Missions.Commands.UpdateMission;

public class UpdateMissionCommandValidator : UpdateCommandValidator<UpdateMissionCommand>
{
    public UpdateMissionCommandValidator()
    {
        RuleFor(x => x.Title).MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(100);
    }
}
