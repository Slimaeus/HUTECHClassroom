using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Common.Mappings;

public sealed class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Class, string>()
            .ConvertUsing(x => x.Name);
        CreateMap<ApplicationUser, MemberDTO>()
            .ForMember(x => x.AvatarUrl, o => o.MapFrom(x => x.Avatar != null ? x.Avatar.Url : null));
        CreateMap<Faculty, UserFacultyDTO>();
    }
}
