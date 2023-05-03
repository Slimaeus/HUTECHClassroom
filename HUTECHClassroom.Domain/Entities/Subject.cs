using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Subject : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int TotalCredits { get; set; }

    public Guid MajorId { get; set; }
    public Major Major { get; set; } = default!;
}
