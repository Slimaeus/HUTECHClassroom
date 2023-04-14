using FluentValidation;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;

namespace HUTECHClassroom.Application.Missions.Commands
{
    public class UpdateMissionCommandValidator : AbstractValidator<UpdateMissionCommand>
    {
        public UpdateMissionCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
