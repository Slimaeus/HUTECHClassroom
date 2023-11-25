using HUTECHClassroom.Application.Scores.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;
public sealed record ClassroomScoreDTO(int OrdinalNumber, double? Score, ScoreTypeDTO? ScoreType);