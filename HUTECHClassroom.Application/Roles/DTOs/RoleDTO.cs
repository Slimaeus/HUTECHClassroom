using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Roles.DTOs;

public record RoleDTO : IEntityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
