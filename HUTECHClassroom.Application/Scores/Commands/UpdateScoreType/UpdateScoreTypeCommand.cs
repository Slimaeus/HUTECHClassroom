using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.ScoreTypes.Commands.UpdateScoreType;

public sealed record UpdateScoreTypeCommand(int Id, string? Name) : UpdateCommand<int>(Id);
public sealed class UpdateScoreTypeCommandHandler : UpdateCommandHandler<int, ScoreType, UpdateScoreTypeCommand>
{
    public UpdateScoreTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
