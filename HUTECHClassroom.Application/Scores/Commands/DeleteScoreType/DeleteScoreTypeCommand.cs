using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Scores.DTOs;

namespace HUTECHClassroom.Application.ScoreTypes.Commands.DeleteScoreType;

public sealed record DeleteScoreTypeCommand(int Id) : DeleteCommand<int, ScoreTypeDTO>(Id);
public sealed class DeleteScoreTypeCommandHandler : DeleteCommandHandler<int, ScoreType, DeleteScoreTypeCommand, ScoreTypeDTO>
{
    public DeleteScoreTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
