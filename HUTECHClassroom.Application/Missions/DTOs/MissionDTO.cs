using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Missions.DTOs;

public record MissionDTO : BaseEntityDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; } = false;

    public MissionProjectDTO Project { get; set; }
}
