using AutoMapper;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.ApplicationUsers;
using HUTECHClassroom.Web.ViewModels.Classrooms;

namespace HUTECHClassroom.Web.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ImportedUserViewModel, ApplicationUser>()
            .ForMember(u => u.Id, options => options.MapFrom(vm => Guid.NewGuid()))
            .ForMember(u => u.Email, options => options.MapFrom(vm => $"{vm.UserName}@test.com"));

        CreateMap<ImportedClassroomViewModel, Classroom>();
    }
}
