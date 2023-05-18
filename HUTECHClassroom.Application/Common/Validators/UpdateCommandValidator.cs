using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators;

public class UpdateCommandValidator<TKey, TCommand> : AbstractValidator<TCommand>
    where TCommand : UpdateCommand<TKey>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
public class UpdateCommandValidator<TCommand> : UpdateCommandValidator<Guid, TCommand>
    where TCommand : UpdateCommand<Guid>
{ }