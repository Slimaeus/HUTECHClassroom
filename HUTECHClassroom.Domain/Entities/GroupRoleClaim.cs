using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class GroupRoleClaim : IdentityRoleClaim<Guid>
{
    public Guid GroupRoleId { get; set; }
    public GroupRole GroupRole { get; set; }

}
