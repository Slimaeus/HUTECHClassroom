using FluentValidation;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators;

public class DeleteRangeCommandValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : DeleteRangeCommand
{
    public DeleteRangeCommandValidator()
    {
        RuleFor(x => x.Ids).NotEmpty().NotNull();
    }
}