using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandValidator : UpdateCommandValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(100);
    }
}
