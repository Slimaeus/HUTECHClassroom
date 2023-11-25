namespace HUTECHClassroom.Domain.Entities;

public class StudentResult
{
    public Guid? StudentId { get; set; }
    public virtual ApplicationUser? Student { get; set; }
    public Guid? ClassroomId { get; set; }
    public virtual Classroom? Classroom { get; set; }
    public int? ScoreTypeId { get; set; }
    public virtual ScoreType? ScoreType { get; set; }

    public int OrdinalNumber { get; set; }
    public double Score { get; set; }
}
