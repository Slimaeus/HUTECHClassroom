namespace HUTECHClassroom.Domain.Entities;

public class ExerciseUser
{
    public Guid ExerciseId { get; set; }
    public virtual Exercise Exercise { get; set; }
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}
