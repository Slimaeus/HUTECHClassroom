using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class GroupRoleClaim : IdentityRoleClaim<Guid>
{
    public GroupRole Role { get; set; }

}
