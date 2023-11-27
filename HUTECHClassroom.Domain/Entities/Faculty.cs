using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Faculty : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Classroom> Classrooms { get; set; } = new HashSet<Classroom>();
    public virtual ICollection<Major> Majors { get; set; } = new HashSet<Major>();
    public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
}
