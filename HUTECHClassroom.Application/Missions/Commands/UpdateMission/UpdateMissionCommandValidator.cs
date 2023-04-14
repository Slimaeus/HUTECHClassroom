using FluentValidation;

namespace HUTECHClassroom.Application.Missions.Commands.UpdateMission
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
