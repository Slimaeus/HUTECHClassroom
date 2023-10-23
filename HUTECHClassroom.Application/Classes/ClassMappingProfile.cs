using HUTECHClassroom.Application.Classes.DTOs;

namespace HUTECHClassroom.Application.Classrooms;

public class ClassMappingProfile : Profile
{
    public ClassMappingProfile()
    {
        CreateMap<Class, ClassDTO>();
        //CreateMap<CreateClassCommand, Class>();
        //CreateMap<UpdateClassCommand, Class>()
        //    .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
    }
}
