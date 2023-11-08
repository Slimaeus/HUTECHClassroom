using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.ScoreTypes.Commands.UpdateScoreType;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.ScoreTyps.Commands.UpdateScoreTyp;

public sealed class UpdateScoreTypeCommandValidator : UpdateCommandValidator<int, UpdateScoreTypeCommand>
{
    public UpdateScoreTypeCommandValidator()
        => RuleFor(x => x.Name).MaximumLength(ScoreTypeConstants.NAME_MAX_LENGTH);
}
