using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Missions.DTOs;

public record MissionProjectDTO : BaseEntityDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}
