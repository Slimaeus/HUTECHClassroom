using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Roles.DTOs;

public record RoleDTO : BaseEntityDTO
{
    public string Name { get; set; }
}
