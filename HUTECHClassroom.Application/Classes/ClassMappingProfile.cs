using HUTECHClassroom.Application.Classes.Commands.CreateClass;
using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Classs.Commands.UpdateClass;

namespace HUTECHClassroom.Application.Classrooms;

public sealed class ClassMappingProfile : Profile
{
    public ClassMappingProfile()
    {
        CreateMap<Class, ClassDTO>();
        CreateMap<CreateClassCommand, Class>();
        CreateMap<UpdateClassCommand, Class>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
    }
}
