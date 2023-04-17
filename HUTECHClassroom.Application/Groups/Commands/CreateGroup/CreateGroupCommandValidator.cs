using FluentValidation;

namespace HUTECHClassroom.Application.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
