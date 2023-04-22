using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Missions.Commands.DeleteRangeMission;

public record DeleteRangeMissionCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeMissionCommandHandler : DeleteRangeCommandHandler<Mission, DeleteRangeMissionCommand>
{
    public DeleteRangeMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
