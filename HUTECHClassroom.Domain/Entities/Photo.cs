using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Photo : BaseEntity
{
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public Guid? UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}
