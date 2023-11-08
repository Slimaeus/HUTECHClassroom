using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public sealed class Class : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    public ICollection<Classroom> Classrooms { get; set; } = new HashSet<Classroom>();
}
