using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Missions.DTOs;

public record MissionProjectDTO : BaseEntityDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public GroupDTO Group { get; set; }
}
