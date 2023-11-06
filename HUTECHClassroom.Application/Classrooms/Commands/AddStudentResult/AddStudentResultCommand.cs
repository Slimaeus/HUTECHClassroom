using HUTECHClassroom.Application.Scores.DTOs;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddStudentResult;

public sealed record AddStudentResultCommand(IList<StudentResultDTO> StudentResults) : IRequest<Unit>;