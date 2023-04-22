using HUTECHClassroom.Application.Answers.Commands.CreateAnswer;
using HUTECHClassroom.Application.Answers.Commands.UpdateAnswer;
using HUTECHClassroom.Application.Answers.DTOs;

namespace HUTECHClassroom.Application.Answers;

public class AnswerMappingProfile : Profile
{
    public AnswerMappingProfile()
    {
        CreateMap<Answer, AnswerDTO>();
        CreateMap<CreateAnswerCommand, Answer>();
        CreateMap<UpdateAnswerCommand, Answer>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
    }
}
