using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.ScoreTypes.Commands.CreateScoreType;

public sealed class CreateScoreTypeCommandValidator : AbstractValidator<CreateScoreTypeCommand>
{
    public CreateScoreTypeCommandValidator()
        => RuleFor(x => x.Name)
        .NotNull()
        .NotEmpty()
        .MaximumLength(ScoreTypeConstants.NAME_MAX_LENGTH);
}
