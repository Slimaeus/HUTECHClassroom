using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators;

public class DeleteRangeCommandValidator<TKey, TCommand> : AbstractValidator<TCommand>
    where TCommand : DeleteRangeCommand<TKey>
{
    public DeleteRangeCommandValidator()
    {
        RuleFor(x => x.Ids).NotEmpty().NotNull();
    }
}
public class DeleteRangeCommandValidator<TCommand> : DeleteRangeCommandValidator<Guid, TCommand>
    where TCommand : DeleteRangeCommand<Guid>
{ }