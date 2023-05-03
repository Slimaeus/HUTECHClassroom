using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Answer : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public string? Link { get; set; }
    public float Score { get; set; }

    public Guid UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise? Exercise { get; set; }
}
