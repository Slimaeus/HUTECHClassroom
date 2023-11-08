using HUTECHClassroom.Application.Subjects.Commands.CreateSubject;
using HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;
using HUTECHClassroom.Application.Subjects.DTOs;

namespace HUTECHClassroom.Application.Subjects;

public sealed class SubjectMappingProfile : Profile
{
    public SubjectMappingProfile()
    {
        CreateMap<Subject, SubjectDTO>();
        CreateMap<CreateSubjectCommand, Subject>();
        CreateMap<UpdateSubjectCommand, Subject>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<Major, SubjectMajorDTO>();
    }
}
