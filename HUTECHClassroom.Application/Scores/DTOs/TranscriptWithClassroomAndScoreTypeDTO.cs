using HUTECHClassroom.Application.Classrooms.DTOs;

namespace HUTECHClassroom.Application.Scores.DTOs;

public sealed record TranscriptWithClassroomAndScoreTypeDTO(
    IEnumerable<StudentResultDTO> Transcript,
    ClassroomDTO Classroom,
    ScoreTypeDTO ScoreType);