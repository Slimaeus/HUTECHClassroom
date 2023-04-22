using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Missions.Commands.UpdateMission;

public record UpdateMissionCommand(Guid Id) : UpdateCommand(Id)
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
}
public class UpdateMissionCommandHandler : UpdateCommandHandler<Mission, UpdateMissionCommand>
{
    public UpdateMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
