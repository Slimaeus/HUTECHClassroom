using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Users.DTOs;

namespace HUTECHClassroom.Application.Scores.DTOs;

public sealed record StudentResultDTO(int Score, UserDTO Student, ClassroomDTO Classroom, ScoreTypeDTO ScoreType);
