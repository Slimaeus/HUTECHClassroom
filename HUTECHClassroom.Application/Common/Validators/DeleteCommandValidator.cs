using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators;

public class DeleteCommandValidator<TKey, TCommand, TDTO> : AbstractValidator<TCommand>
    where TCommand : DeleteCommand<TKey, TDTO>
    where TDTO : class
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
public class DeleteCommandValidator<TCommand, TDTO> : DeleteCommandValidator<Guid, TCommand, TDTO>
    where TCommand : DeleteCommand<Guid, TDTO>
    where TDTO : class
{
}
