using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Projects.DTOs;

public sealed record ProjectDTO : BaseEntityDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public GroupDTO? Group { get; set; }
}
