using FluentValidation;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Missions.Commands.UpdateMission
{
    public class UpdateMissionCommandValidator : UpdateCommandValidator<UpdateMissionCommand>
    {
        public UpdateMissionCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
