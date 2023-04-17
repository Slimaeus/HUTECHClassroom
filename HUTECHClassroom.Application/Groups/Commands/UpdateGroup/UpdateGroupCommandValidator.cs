using FluentValidation;

namespace HUTECHClassroom.Application.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
