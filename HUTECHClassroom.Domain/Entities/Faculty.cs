using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Faculty : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Classroom> Classrooms { get; set; } = new HashSet<Classroom>();
    public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
}
