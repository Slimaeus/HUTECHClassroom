using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators;

public class DeleteCommandValidator<TCommand, TDTO> : AbstractValidator<TCommand>
    where TCommand : DeleteCommand<TDTO>
    where TDTO : class
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
