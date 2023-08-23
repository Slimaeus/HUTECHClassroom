using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Photo : BaseEntity
{
    public string PublicId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}
