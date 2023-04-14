using AutoMapper;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Common.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region User
            CreateMap<ApplicationUser, MemberDTO>();
            #endregion

            #region Missions
            CreateMap<Mission, MissionDTO>();
            CreateMap<CreateMissionCommand, Mission>();
            CreateMap<UpdateMissionCommand, Mission>()
                .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
            #endregion
        }
    }
}
