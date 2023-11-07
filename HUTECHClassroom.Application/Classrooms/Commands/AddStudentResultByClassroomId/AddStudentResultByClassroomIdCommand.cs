using HUTECHClassroom.Application.Scores.DTOs;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddStudentResultByClassroomId;

public sealed record AddStudentResultByClassroomIdCommand(Guid ClassroomId, IList<StudentResultDTO> StudentResults) : IRequest<Unit>;