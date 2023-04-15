using HUTECHClassroom.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public virtual ICollection<MissionUser> MissionUsers { get; set; }
    }
}
