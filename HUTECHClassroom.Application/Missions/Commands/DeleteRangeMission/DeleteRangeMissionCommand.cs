using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Missions.Commands.DeleteRangeMission;

public record DeleteRangeMissionCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public sealed class DeleteRangeMissionCommandHandler : DeleteRangeCommandHandler<Mission, DeleteRangeMissionCommand>
{
    public DeleteRangeMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
