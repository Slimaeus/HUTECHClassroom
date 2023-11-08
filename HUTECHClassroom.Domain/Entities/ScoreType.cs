using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public sealed class ScoreType : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}
