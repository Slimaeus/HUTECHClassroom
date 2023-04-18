using AutoMapper;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Missions;

public class MissionMappingProfile : Profile
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
            .ConstructUsing(x => new MemberDTO(x.User.UserName, x.User.Email));
    }
}
