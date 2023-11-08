using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.ScoreTypes.Commands.CreateScoreType;

public sealed record CreateScoreTypeCommand(string Name) : CreateCommand<int>;
public sealed class CreateScoreTypeCommandHandler : CreateCommandHandler<int, ScoreType, CreateScoreTypeCommand>
{

    public CreateScoreTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
