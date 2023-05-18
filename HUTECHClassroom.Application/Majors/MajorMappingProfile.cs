using HUTECHClassroom.Application.Majors.Commands.CreateMajor;
using HUTECHClassroom.Application.Majors.Commands.UpdateMajor;
using HUTECHClassroom.Application.Majors.DTOs;

namespace HUTECHClassroom.Application.Majors;

public class MajorMappingProfile : Profile
{
    public MajorMappingProfile()
    {
        CreateMap<Major, MajorDTO>();
        CreateMap<CreateMajorCommand, Major>();
        CreateMap<UpdateMajorCommand, Major>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
    }
}
