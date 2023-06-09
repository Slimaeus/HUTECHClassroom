using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.GroupRoles.DTOs;

public record GroupRoleDTO(Guid Id, string Name) : IEntityDTO
{ }
