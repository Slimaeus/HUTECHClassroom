using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Roles.DTOs;

public record RoleDTO(Guid Id, string Name) : IEntityDTO
{ }
