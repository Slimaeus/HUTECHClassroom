using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Application.Scores.DTOs;

public sealed record TranscriptFileWithClassroomIdAndScoreTypeIdDTO(IFormFile File, Guid ClassroomId, int ScoreTypeId);