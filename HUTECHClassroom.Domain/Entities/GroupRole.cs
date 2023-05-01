using HUTECHClassroom.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class GroupRole : IdentityRole<Guid>, IEntity
{
    public GroupRole()
    {

    }
    public GroupRole(string roleName) : base(roleName)
    {
    }
    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new HashSet<GroupUser>();
    public virtual ICollection<GroupRoleClaim> GroupRoleClaims { get; set; } = new HashSet<GroupRoleClaim>();
}
