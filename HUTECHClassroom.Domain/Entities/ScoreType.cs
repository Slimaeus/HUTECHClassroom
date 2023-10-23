using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class ScoreType : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}
