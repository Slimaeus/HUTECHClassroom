using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Scores.DTOs;

public sealed record ScoreTypeDTO(string Name) : BaseEntityDTO<int>;