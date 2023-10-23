namespace HUTECHClassroom.Domain.Entities;

public class StudentScore
{
    public Guid? StudentId { get; set; }
    public ApplicationUser? Student { get; set; }
    public Guid? ClassroomId { get; set; }
    public Classroom? Classroom { get; set; }
    public int? ScoreTypeId { get; set; }
    public ScoreType? ScoreType { get; set; }

    public int Score { get; set; }
}
