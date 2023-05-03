using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Major : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int TotalCredits { get; set; }
    public int NonComulativeCredits { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
}
