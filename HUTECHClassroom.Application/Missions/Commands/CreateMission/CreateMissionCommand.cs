using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Missions.Commands.CreateMission;

public sealed record CreateMissionCommand : CreateCommand
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsDone { get; set; } = false;
    public Guid ProjectId { get; set; }
}
public sealed class CreateMissionCommandHandler : CreateCommandHandler<Mission, CreateMissionCommand>
{
    public CreateMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
