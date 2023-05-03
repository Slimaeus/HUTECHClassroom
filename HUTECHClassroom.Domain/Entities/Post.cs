using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Post : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public string? Link { get; set; }

    public Guid ClassroomId { get; set; }
    public virtual Classroom? Classroom { get; set; }
    public Guid UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}
