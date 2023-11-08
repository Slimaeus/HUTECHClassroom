using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.ScoreTypes.Commands.DeleteRangeScoreType;

public record DeleteRangeScoreTypeCommand(IList<int> Ids) : DeleteRangeCommand<int>(Ids);
public sealed class DeleteRangeScoreTypeCommandHandler : DeleteRangeCommandHandler<int, ScoreType, DeleteRangeScoreTypeCommand>
{
    public DeleteRangeScoreTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
