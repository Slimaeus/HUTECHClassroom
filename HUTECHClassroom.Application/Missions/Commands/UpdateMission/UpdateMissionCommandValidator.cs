using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;

namespace HUTECHClassroom.Application.Missions.Commands;

public class UpdateMissionCommandValidator : UpdateCommandValidator<UpdateMissionCommand>
{
    public UpdateMissionCommandValidator()
    {
        RuleFor(x => x.Title).MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(100);
    }
}
