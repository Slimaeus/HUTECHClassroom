using HUTECHClassroom.Application.Scores.Commands.AddStudentResult;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Application.ScoreTypes.Commands.CreateScoreType;
using HUTECHClassroom.Application.ScoreTypes.Commands.UpdateScoreType;

namespace HUTECHClassroom.Application.Scores;

public sealed class ScoreMappingProfile : Profile
{
    public ScoreMappingProfile()
    {
        CreateMap<ScoreType, ScoreTypeDTO>();
        CreateMap<CreateScoreTypeCommand, ScoreType>();
        CreateMap<UpdateScoreTypeCommand, ScoreType>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<StudentResult, StudentResultDTO>();

        CreateMap<AddStudentResultCommand, StudentResult>();
    }
}
