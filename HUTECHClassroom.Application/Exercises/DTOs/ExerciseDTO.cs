using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Exercises.DTOs;

public record ExerciseDTO : BaseEntityDTO
{
    public string Title { get; set; }
    public string Instruction { get; set; }
    public string Link { get; set; }
    public float TotalScore { get; set; }
    public DateTime Deadline { get; set; }
    public string Topic { get; set; }
    public string Criteria { get; set; }
    public ExerciseClassroomDTO Classroom { get; set; }
}
