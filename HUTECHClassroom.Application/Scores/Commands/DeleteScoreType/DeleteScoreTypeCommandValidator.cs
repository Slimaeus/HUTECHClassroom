using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Scores.DTOs;

namespace HUTECHClassroom.Application.ScoreTypes.Commands.DeleteScoreType;

public sealed class DeleteScoreTypeCommandValidator : DeleteCommandValidator<int, DeleteScoreTypeCommand, ScoreTypeDTO>
{
}
