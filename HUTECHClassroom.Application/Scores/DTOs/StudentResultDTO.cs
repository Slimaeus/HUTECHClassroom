using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Users.DTOs;

namespace HUTECHClassroom.Application.Scores.DTOs;

public sealed record StudentResultDTO(int OrdinalNumber, double Score, UserDTO? Student, ClassroomDTO? Classroom, ScoreTypeDTO? ScoreType, string? StudentId = null, string? ClassroomId = null, int? ScoreTypeId = null)
{
    public StudentResultDTO() : this(0, 0, null, null, null)
    {

    }
}
