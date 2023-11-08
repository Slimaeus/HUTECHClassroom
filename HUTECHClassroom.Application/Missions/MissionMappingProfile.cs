using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Missions.Commands.AddMissionUser;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;

namespace HUTECHClassroom.Application.Missions;

public sealed class MissionMappingProfile : Profile
{
    public MissionMappingProfile()
    {
        CreateMap<Mission, MissionDTO>()
            .ForMember(x => x.Project, options => options.MapFrom(x => x.Project));
        CreateMap<CreateMissionCommand, Mission>();
        CreateMap<UpdateMissionCommand, Mission>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<Project, MissionProjectDTO>();

        CreateMap<MissionUser, MemberDTO>()
            .ConstructUsing(x => new MemberDTO(x.UserId, x.User != null ? x.User.UserName : null, x.User != null ? x.User.Email : null, x.User != null ? x.User.FirstName : null, x.User != null ? x.User.LastName : null, x.User != null && x.User.Class != null ? x.User.Class.Name : null!, x.User != null && x.User.Avatar != null ? x.User.Avatar.Url : string.Empty));

        CreateMap<AddMissionUserCommand, MissionUser>();
    }
}
