using FluentValidation;

namespace HUTECHClassroom.Application.Common.Requests
{
    public abstract class UpdateCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : UpdateCommand
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
