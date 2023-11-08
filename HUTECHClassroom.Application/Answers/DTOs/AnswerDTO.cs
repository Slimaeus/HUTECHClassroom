using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Answers.DTOs;

public record AnswerDTO : BaseEntityDTO
{
    public string? Description { get; set; }
    public string? Link { get; set; }
    public float Score { get; set; }
    public MemberDTO? User { get; set; }
    public ExerciseDTO? Exercise { get; set; }
}
