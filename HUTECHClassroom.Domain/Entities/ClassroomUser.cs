namespace HUTECHClassroom.Domain.Entities;

public class ClassroomUser
{
    public Guid ClassroomId { get; set; }
    public virtual Classroom Classroom { get; set; }
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}
