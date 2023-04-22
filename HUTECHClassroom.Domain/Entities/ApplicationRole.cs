using HUTECHClassroom.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>, IEntity
{
    public ApplicationRole()
    {

    }
    public ApplicationRole(string roleName) : base(roleName)
    {
    }

    public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
}
