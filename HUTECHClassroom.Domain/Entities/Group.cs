using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid LeaderId { get; set; }
    public virtual ApplicationUser Leader { get; set; }
    public Guid ClassroomId { get; set; }
    public virtual Classroom Classroom { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new HashSet<GroupUser>();
}
