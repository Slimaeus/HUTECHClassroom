using HUTECHClassroom.Application.Scores.DTOs;

namespace HUTECHClassroom.Application.Scores;

public sealed class ScoreMappingProfile : Profile
{
    public ScoreMappingProfile()
    {
        CreateMap<ScoreType, ScoreTypeDTO>();

        CreateMap<StudentResult, StudentResultDTO>();
    }
}
