using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Subject : BaseEntity<string>
{
    public string Title { get; set; } = string.Empty;
    public int TotalCredits { get; set; }

    public string? MajorId { get; set; }
    public virtual Major? Major { get; set; }

    public virtual ICollection<Classroom> Classrooms { get; set; } = new HashSet<Classroom>();
}
