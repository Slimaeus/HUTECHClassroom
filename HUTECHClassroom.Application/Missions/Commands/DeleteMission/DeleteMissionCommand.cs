using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;

namespace HUTECHClassroom.Application.Missions.Commands.DeleteMission;

public record DeleteMissionCommand(Guid Id) : DeleteCommand<MissionDTO>(Id);
public sealed class DeleteMissionCommandHandler : DeleteCommandHandler<Mission, DeleteMissionCommand, MissionDTO>
{
    public DeleteMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
