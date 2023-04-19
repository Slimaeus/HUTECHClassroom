using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Exercise : BaseEntity
{
    public string Title { get; set; }
    public string Instruction { get; set; }
    public string Link { get; set; }
    public float TotalScore { get; set; }
    public DateTime Deadline { get; set; }
    public string Topic { get; set; }
    public string Criteria { get; set; }

    public Guid ClassroomId { get; set; }
    public Classroom Classroom { get; set; }

    public virtual ICollection<ExerciseUser> ExerciseUsers { get; set; } = new HashSet<ExerciseUser>();

}
