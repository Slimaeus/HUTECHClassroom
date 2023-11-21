using HUTECHClassroom.Application.Users.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;
public sealed record ClassroomStudentResultDTO(UserDTO Student, ClassroomDTO Classroom, IEnumerable<ClassroomScoreDTO> Scores);