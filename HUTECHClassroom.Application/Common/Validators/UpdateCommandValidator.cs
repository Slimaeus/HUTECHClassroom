using FluentValidation;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators
{
    public class UpdateCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : UpdateCommand
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
