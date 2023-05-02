using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class GroupRoleClaim : IdentityRoleClaim<Guid>
{
    public Guid GroupRoleId { get; set; }
    public virtual GroupRole GroupRole { get; set; }

}
