using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.GroupRoles.DTOs;

public class GroupRoleDTO : IEntityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
