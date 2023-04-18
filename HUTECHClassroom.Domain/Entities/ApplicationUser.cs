using HUTECHClassroom.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public virtual ICollection<MissionUser> MissionUsers { get; set; } = new HashSet<MissionUser>();
        public virtual ICollection<GroupUser> GroupUsers { get; set; } = new HashSet<GroupUser>();
        public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
        public virtual ICollection<ClassroomUser> ClassroomUsers { get; set; } = new HashSet<ClassroomUser>();
    }
}
